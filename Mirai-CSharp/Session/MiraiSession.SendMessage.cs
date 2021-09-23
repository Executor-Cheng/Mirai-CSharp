using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Builders;
using Mirai.CSharp.Models.ChatMessages;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task RevokeMessageAsync(int messageId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendFriendMessageAsync(long qqNumber, params IChatMessage[] chain)
        {
            return SendFriendMessageAsync(qqNumber, chain, default);
        }

        /// <inheritdoc/>
        public virtual Task<int> SendFriendMessageAsync(long qqNumber, IChatMessage[] chain, CancellationToken token = default)
        {
            return SendFriendMessageAsync(qqNumber, chain, null, token);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendFriendMessageAsync(long qqNumber, IMessageChainBuilder builder, CancellationToken token = default)
        {
            return SendFriendMessageAsync(qqNumber, builder, null, token);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendFriendMessageAsync(long qqNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendGroupMessageAsync(long groupNumber, params IChatMessage[] chain)
        {
            return SendGroupMessageAsync(groupNumber, chain, default);
        }

        /// <inheritdoc/>
        public virtual Task<int> SendGroupMessageAsync(long groupNumber, IChatMessage[] chain, CancellationToken token = default)
        {
            return SendGroupMessageAsync(groupNumber, chain, null, token);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendGroupMessageAsync(long groupNumber, IMessageChainBuilder builder, CancellationToken token = default)
        {
            return SendGroupMessageAsync(groupNumber, builder, null, token);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendGroupMessageAsync(long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, params IChatMessage[] chain)
        {
            return SendTempMessageAsync(qqNumber, qqNumber, chain, default);
        }

        /// <inheritdoc/>
        public virtual Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IChatMessage[] chain, CancellationToken token = default)
        {
            return SendTempMessageAsync(qqNumber, qqNumber, chain, null, default);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IChatMessage[] chain, int? quoteMsgId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageChainBuilder builder, CancellationToken token = default)
        {
            return SendTempMessageAsync(qqNumber, qqNumber, builder, null, default);
        }

        /// <inheritdoc/>
        public abstract Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, IMessageChainBuilder builder, int? quoteMsgId, CancellationToken token = default);
    }
}
