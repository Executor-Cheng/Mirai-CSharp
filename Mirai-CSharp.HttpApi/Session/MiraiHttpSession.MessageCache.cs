using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Utility;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        /// <remarks>
        /// 当缓存失效, 或者未注册用于解析消息的 <see cref="IMiraiHttpMessageParser{TMessage}"/> 时, 本异步方法将返回 <see langword="null"/>
        /// </remarks>
        [Obsolete("自mirai-api-http 2.6.0起, 要求传入消息所在的群号或好友QQ号, 请考虑调用RetriveMessageAsync(int, long, CancellationToken)方法")]
        public Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            if (session.ApiVersion >= new Version(2, 6, 0))
            {
                throw new NotSupportedException("自mirai-api-http 2.6.0起, 要求传入消息所在的群号或好友QQ号, 请考虑调用RetriveMessageAsync(int, long, CancellationToken)方法");
            }
            return RetriveMessageAsync(session, $"{_options.BaseUrl}/messageFromId?sessionKey={session.SessionKey}&id={messageId}", token);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// 当缓存失效, 或者未注册用于解析消息的 <see cref="IMiraiHttpMessageParser{TMessage}"/> 时, 本异步方法将返回 <see langword="null"/>
        /// </remarks>
        public Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, long target, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return RetriveMessageAsync(session, $"{_options.BaseUrl}/messageFromId?sessionKey={session.SessionKey}&id={messageId}&target={target}", token);
        }

        private async Task<IMiraiHttpMessage?> RetriveMessageAsync(InternalSessionInfo session, string url, CancellationToken token = default)
        {
            IMiraiHttpMessageParserResolver resolver = _services.GetRequiredService<IMiraiHttpMessageParserResolver>();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            JsonElement root = await _client.GetAsync(url, token)
                .GetObjectAsync<JsonElement>(token)
                .DisposeWhenCompleted(cts)
                .ConfigureAwait(false);
            root.EnsureApiRespCode();
            JsonElement data = root.GetProperty("data");
            IMiraiHttpMessageParser? parser = resolver.ResolveParser(in data);
            if (parser != null && parser.CanParse(in data))
            {
                return (IMiraiHttpMessage)parser.Parse(in data);
            }
            return null;
        }

        /// <inheritdoc/>
        public virtual Task<IChatMessage[][]> GetChatHistoryAsync(long target, DateTime? from, DateTime? to, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                timeStart = from.HasValue ? Utils.DateTime2UnixTimeSeconds(from.Value) : 0,
                timeEnd = to.HasValue ? Utils.DateTime2UnixTimeSeconds(to.Value) : 0,
                target
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/roamingMessages", payload, token)
                .AsApiRespAsync<IChatMessage[][]>(_chatMessageSerializingOptions)
                .DisposeWhenCompleted(cts);
        }
    }
}
