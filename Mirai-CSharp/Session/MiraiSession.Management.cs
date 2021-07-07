using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp.Models;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task<IFriendInfo[]> GetFriendListAsync(CancellationToken token = default);

        public abstract Task<IGroupConfig> GetGroupConfigAsync(long groupNumber, CancellationToken token = default);

        public abstract Task<IGroupInfo[]> GetGroupListAsync(CancellationToken token = default);

        public abstract Task MuteAllAsync(long groupNumber, CancellationToken token = default);

        public abstract Task MuteAsync(long memberId, long groupNumber, TimeSpan duration, CancellationToken token = default);

        public abstract Task UnmuteAllAsync(long groupNumber, CancellationToken token = default);

        public abstract Task UnmuteAsync(long memberId, long groupNumber, CancellationToken token = default);

        public abstract Task KickMemberAsync(long memberId, long groupNumber, string msg = "您已被移出群聊", CancellationToken token = default);

        public abstract Task LeaveGroupAsync(long groupNumber, CancellationToken token = default);

        public abstract Task ChangeGroupConfigAsync(long groupNumber, IGroupConfig config, CancellationToken token = default);

        public abstract Task ChangeGroupMemberInfoAsync(long memberId, long groupNumber, IGroupMemberCardInfo info, CancellationToken token = default);

        public abstract Task<IGroupMemberCardInfo> GetGroupMemberInfoAsync(long memberId, long groupNumber, CancellationToken token = default);

        public abstract Task<IGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber, CancellationToken token = default);
    }
}
