using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using IMessageChainBuilder = Mirai.CSharp.Builders.IMessageChainBuilder;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        private async Task<int> CommonSendMessageAsync(string action, long? qqNumber, long? groupNumber, ISharedChatMessage[] chain, int? quoteMsgId, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            if (chain == null || chain.Length == 0)
            {
                throw new ArgumentException("消息链必须为非空且至少有1条消息。");
            }
            int emptyPlainCount = 0;
            for (int i = 0; i < chain.Length; i++)
            {
                ISharedChatMessage msg = chain[i];
                if (msg is not IChatMessage)
                {
                    throw new ArgumentException($"不允许发送未实现 {typeof(IChatMessage).FullName} 接口的消息。");
                }
                if (msg is ISourceMessage)
                {
                    throw new ArgumentException("无法发送基本信息(SourceMessage)。");
                }
                if (msg is IQuoteMessage)
                {
                    throw new ArgumentException("无法发送引用信息(QuoteMessage), 请使用quoteMsgId参数进行引用。");
                }
                if (msg is IPlainMessage pm && string.IsNullOrEmpty(pm.Message))
                {
                    emptyPlainCount++;
                }
            }
            if (emptyPlainCount == chain.Length)
            {
                throw new ArgumentException("消息链仅含文本消息且所有文本均为空。");
            }
            // 前边已经检查了所有元素均实现了 IChatMessage 接口, 但直接强制转换数组类型会引发 InvalidCastException, 所以玩一点 trick
            IEnumerable<IChatMessage> convertedChain = Unsafe.As<ISharedChatMessage[], IEnumerable<IChatMessage>>(ref chain);
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                qq = qqNumber,
                group = groupNumber,
                quote = quoteMsgId,
                messageChain = convertedChain
            };
            using JsonDocument j = await _client.PostAsJsonAsync($"{_options.BaseUrl}/{action}", payload, _chatMessageSerializingOptions, token).GetJsonAsync(token).DisposeWhenCompleted(cts).ConfigureAwait(false);
            JsonElement root = j.RootElement;
            root.EnsureApiRespCode(out _);
            return root.GetProperty("messageId").GetInt32();
        }

        /// <inheritdoc/>
        public override Task<int> SendFriendMessageAsync(long qqNumber, ISharedChatMessage[] chain, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, chain, quoteMsgId, token);
        }
        /// <inheritdoc/>
        public override Task<int> SendFriendMessageAsync(long qqNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, builder.Build(), quoteMsgId, token);
        }

        /// <inheritdoc/>
        public override Task<int> SendGroupMessageAsync(long groupNumber, ISharedChatMessage[] chain, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, chain, quoteMsgId, token);
        }
        /// <inheritdoc/>
        public override Task<int> SendGroupMessageAsync(long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, builder.Build(), quoteMsgId, token);
        }

        /// <inheritdoc/>
        public override Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, ISharedChatMessage[] chain, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, groupNumber, chain, quoteMsgId, token);
        }
        /// <inheritdoc/>
        public override Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, groupNumber, builder.Build(), quoteMsgId, token);
        }

        /// <inheritdoc/>
        public override Task RevokeMessageAsync(int messageId, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = messageId
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/recall", payload, token).AsApiRespAsync(token).DisposeWhenCompleted(cts);
        }
    }
}
