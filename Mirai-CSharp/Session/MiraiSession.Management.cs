using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task<IFriendInfo[]> GetFriendListAsync(CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IGroupConfig> GetGroupConfigAsync(long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IGroupInfo[]> GetGroupListAsync(CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task MuteAllAsync(long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task MuteAsync(long memberId, long groupNumber, TimeSpan duration, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task UnmuteAllAsync(long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task UnmuteAsync(long memberId, long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task KickMemberAsync(long memberId, long groupNumber, string msg = "您已被移出群聊", CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task LeaveGroupAsync(long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task DeleteFriendAsync(long qqNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task SetEssenceMessageAsync(int id, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task ChangeGroupConfigAsync(long groupNumber, IGroupConfig config, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task ChangeGroupMemberInfoAsync(long memberId, long groupNumber, IGroupMemberCardInfo info, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IGroupMemberInfo> GetGroupMemberInfoAsync(long memberId, long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task SetGroupAdminAsync(long memberId, long groupNumber, bool assign, CancellationToken token = default);
    }
}
