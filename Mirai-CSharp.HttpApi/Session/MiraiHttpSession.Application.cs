using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.EventArgs;
using System.Threading;
using System.Threading.Tasks;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string? message = null, CancellationToken token = default)
        {
            return CommonHandleApplyAsync("newFriendRequestEvent", args, (int)action, message, token);
        }

        /// <inheritdoc/>
        public override Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default)
        {
            return CommonHandleApplyAsync("memberJoinRequestEvent", args, (int)action, message, token);
        }

        /// <inheritdoc/>
        public override Task HandleBotInvitedJoinGroupAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default)
        {
            return CommonHandleApplyAsync("botInvitedJoinGroupRequestEvent", args, (int)action, message, token);
        }

        /// <summary>
        /// 内部使用
        /// </summary>
        private Task CommonHandleApplyAsync(string actPath, IApplyResponseArgs args, int action, string? message, CancellationToken token)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                eventId = args.EventId,
                fromId = args.FromQQ,
                groupId = args.FromGroup,
                operate = action,
                message = message ?? "",
            };
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/resp/{actPath}", payload, token).AsApiRespAsync(token).DisposeWhenCompleted(cts);
        }
    }
}
