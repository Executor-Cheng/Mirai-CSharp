using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Builders;
using Mirai.CSharp.Exceptions;
using Mirai.CSharp.Models.ChatMessages;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">
        /// 请提供以下之一
        /// <list type="bullet">
        /// <item><see cref="ISourceMessage.Id"/></item>
        /// <item><see cref="SendFriendMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendTempMessageAsync(long, long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendGroupMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// </list>
        /// </param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        Task RevokeMessageAsync(int messageId, CancellationToken token = default);

        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">
        /// 请提供以下之一
        /// <list type="bullet">
        /// <item><see cref="ISourceMessage.Id"/></item>
        /// <item><see cref="SendFriendMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendTempMessageAsync(long, long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// <item><see cref="SendGroupMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/> 的返回值</item>
        /// </list>
        /// </param>
        /// <param name="target">消息来源群号/好友QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        Task RevokeMessageAsync(int messageId, long target, CancellationToken token = default);

        /// <inheritdoc cref="SendFriendMessageAsync(long, IChatMessage[], CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, params IChatMessage[] chain);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendFriendMessageAsync(long, IChatMessage[], int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendFriendMessageAsync(long, IChatMessage[], int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IChatMessage[] chain, CancellationToken token = default);
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendFriendMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendFriendMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendFriendMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageChainBuilder builder, CancellationToken token = default);
        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="builder">构建完毕的 <see cref="IMessageChainBuilder"/></param>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendFriendMessageAsync(long qqNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc cref="SendGroupMessageAsync(long, IChatMessage[], CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, params IChatMessage[] chain);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendGroupMessageAsync(long, IChatMessage[], int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendGroupMessageAsync(long, IChatMessage[], int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IChatMessage[] chain, CancellationToken token = default);
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendGroupMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendGroupMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendGroupMessageAsync(long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageChainBuilder builder, CancellationToken token = default);
        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="BotMutedException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="builder">构建完毕的 <see cref="IMessageChainBuilder"/></param>
        /// <param name="groupNumber">目标所在的群号</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendGroupMessageAsync(long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc cref="SendTempMessageAsync(long, long, IChatMessage[], CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, params IChatMessage[] chain);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendTempMessageAsync(long, long, IChatMessage[], int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendTempMessageAsync(long, long, IChatMessage[], int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IChatMessage[] chain, CancellationToken token = default);
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <inheritdoc cref="SendTempMessageAsync(long, long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);
        /// <remarks>
        /// 本方法不会引用回复, 要引用回复, 请调用 <see cref="SendTempMessageAsync(long,long, IMessageChainBuilder, int?, CancellationToken)"/>
        /// </remarks>
        /// <inheritdoc cref="SendTempMessageAsync(long, long, IMessageChainBuilder, int?, CancellationToken)"/>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageChainBuilder builder, CancellationToken token = default);
        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="groupNumber">目标所在的群号</param>
        /// <param name="builder">构建完毕的 <see cref="IMessageChainBuilder"/></param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <returns>用于标识本条消息的 Id</returns>
        Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);
    }
}
