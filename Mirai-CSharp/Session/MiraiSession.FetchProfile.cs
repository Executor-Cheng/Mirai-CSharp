using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task<IBotProfile> GetBotProfileAsync(CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IFriendProfile> GetFriendProfileAsync(long qqNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IGroupMemberProfile> GetGroupMemberProfileAsync(long groupMember, long qqNumber, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<IUserProfile> GetUserProfileAsync(long qqNumber, CancellationToken token = default);
    }
}
