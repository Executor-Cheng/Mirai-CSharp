using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有) // 已经 inheritdocs, 警告无效
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private static readonly JsonSerializerOptions _forSendMsg = CreateSendMsgOpt();
        
        private static JsonSerializerOptions CreateSendMsgOpt()
        {
            JsonSerializerOptions opts = JsonSerializeOptionsFactory.IgnoreNulls;
            opts.Converters.Add(new IMessageBaseArrayConverter());
            return opts;
        }
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <param name="action">api的action</param>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="groupNumber">目标所在的群号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <returns>用于标识本条消息的 Id</returns>
        private async Task<int> CommonSendMessageAsync(string action, long? qqNumber, long? groupNumber, IMessageBase[] chain, int? quoteMsgId)
        {
            InternalSessionInfo session = SafeGetSession();
            if (chain == null || chain.Length == 0)
            {
                throw new ArgumentException("消息链必须为非空且至少有1条消息。");
            }
            if (chain.OfType<SourceMessage>().Any())
            {
                throw new ArgumentException("无法发送基本信息(SourceMessage)。");
            }
            if (chain.OfType<QuoteMessage>().Any())
            {
                throw new ArgumentException("无法发送引用信息(QuoteMessage), 请使用quoteMsgId参数进行引用。");
            }
            if (chain.All(p => p is PlainMessage pm && string.IsNullOrEmpty(pm.Message)))
            {
                throw new ArgumentException("消息链中的所有消息均为空。");
            }
            var payload = new
            {
                sessionKey = session.SessionKey,
                qq = qqNumber,
                group = groupNumber,
                quote = quoteMsgId,
                messageChain = chain
            };
            using JsonDocument j = await session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/{action}", payload, _forSendMsg).GetJsonAsync(token: session.Token);
            JsonElement root = j.RootElement;
            if (root.CheckApiRespCode(out int? code))
            {
                return root.GetProperty("messageId").GetInt32();
            }
            throw GetCommonException(code!.Value, in root);
        }
        
        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendFriendMessageAsync(long, IMessageBase[], int?)"/>
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<int> SendFriendMessageAsync(long qqNumber, params IMessageBase[] chain)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, chain, null);
        }
        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        public Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, chain, quoteMsgId);
        }
        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendFriendMessageAsync(long qqNumber, IMessageBuilder builder, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, builder.Build(), quoteMsgId);
        }

        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendTempMessageAsync(long, long, IMessageBase[], int?)"/>
        /// </remarks>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, params IMessageBase[] chain)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, groupNumber, chain, null);
        }
        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, groupNumber, chain, quoteMsgId);
        }
        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBuilder builder, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, groupNumber, builder.Build(), quoteMsgId);
        }

        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendGroupMessageAsync(long, IMessageBase[], int?)"/>
        /// </remarks>
        /// <exception cref="BotMutedException"/>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendGroupMessageAsync(long groupNumber, params IMessageBase[] chain)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, chain, null);
        }
        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <exception cref="BotMutedException"/>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, chain, quoteMsgId);
        }
        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <exception cref="BotMutedException"/>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <inheritdoc cref="CommonSendMessageAsync"/>
        public Task<int> SendGroupMessageAsync(long groupNumber, IMessageBuilder builder, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, builder.Build(), quoteMsgId);
        }

        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">
        /// 请提供以下之一
        /// <list type="bullet">
        /// <item><see cref="SourceMessage.Id"/></item>
        /// <item><see cref="SendFriendMessageAsync(long, IMessageBase[], int?)"/> 的返回值</item>
        /// <item><see cref="SendTempMessageAsync(long, long, IMessageBase[], int?)"/> 的返回值</item>
        /// <item><see cref="SendGroupMessageAsync(long, IMessageBase[], int?)"/> 的返回值</item>
        /// </list>
        /// </param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        public Task RevokeMessageAsync(int messageId)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = messageId
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/recall", payload, session.Token).AsApiRespAsync(session.Token);
        }
    }
}
