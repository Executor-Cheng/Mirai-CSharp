using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.EventArgs;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task HandleBotInvitedJoinGroupAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string? message = null, CancellationToken token = default);
    }
}
