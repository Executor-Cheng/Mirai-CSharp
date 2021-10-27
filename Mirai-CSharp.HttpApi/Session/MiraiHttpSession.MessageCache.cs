using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Parsers;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        /// <remarks>
        /// 当缓存失效, 或者未注册用于解析消息的 <see cref="IMiraiHttpMessageParser{TMessage}"/> 时, 本异步方法将返回 <see langword="null"/>
        /// </remarks>
        public async Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            IMiraiHttpMessageParserResolver resolver = _services.GetRequiredService<IMiraiHttpMessageParserResolver>();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            JsonElement root = await _client.GetAsync($"{_options.BaseUrl}/messageFromId?sessionKey={session.SessionKey}&id={messageId}", token)
                .GetObjectAsync<JsonElement>(token)
                .DisposeWhenCompleted(cts);
            root.EnsureApiRespCode();
            JsonElement data = root.GetProperty("data");
            IMiraiHttpMessageParser? parser = resolver.ResolveParser(in data);
            if (parser != null && parser.CanParse(in data))
            {
                return (IMiraiHttpMessage)parser.Parse(in data);
            }
            return null;
        }
    }
}
