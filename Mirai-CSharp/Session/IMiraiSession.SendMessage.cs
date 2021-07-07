using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Models;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai_CSharp
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">
        /// 请提供以下之一
        /// <list type="bullet">
        /// <item><see cref="SourceMessage.Id"/></item>
        /// <item><see cref="SendFriendMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendTempMessageAsync(long, long, IMessageBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendGroupMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// </list>
        /// </param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        Task RevokeMessageAsync(int messageId, CancellationToken token = default);

        /// <inheritdoc cref="SendFriendMessageAsync(long, IMessageBase[], int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, CancellationToken token = default);

        /// <inheritdoc cref="SendFriendMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageBuilder builder, CancellationToken token = default);

        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendFriendMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc cref="SendGroupMessageAsync(long, IMessageBase[], int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, CancellationToken token = default);

        /// <inheritdoc cref="SendGroupMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageBuilder builder, CancellationToken token = default);

        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendGroupMessageAsync(long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="BotMutedException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <param name="groupNumber">目标所在的群号</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc cref="SendTempMessageAsync(long, long, IMessageBase[], int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBase[] chain, CancellationToken token = default);

        /// <inheritdoc cref="SendTempMessageAsync(long, long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBuilder builder, CancellationToken token = default);

        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendTempMessageAsync(long, long, IMessageBuilder, int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBase[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="groupNumber">目标所在的群号</param>
        /// <param name="builder">构建完毕的 <see cref="IMessageBuilder"/></param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageBuilder builder, int? quoteMsgId, CancellationToken token = default);
    }
}
