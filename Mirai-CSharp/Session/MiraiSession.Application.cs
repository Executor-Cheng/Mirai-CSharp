using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp.Models;
using Mirai_CSharp.Models.EventArgs;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task HandleBotInvitedJoinGroupAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        public abstract Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        public abstract Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string? message = null, CancellationToken token = default);
    }
}
