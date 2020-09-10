using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using System;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 异步处理添加好友请求
        /// </summary>
        /// <param name="args">收到添加好友申请事件中的参数, 即<see cref="INewFriendApplyEventArgs"/></param>
        /// <inheritdoc cref="CommonHandleApplyAsync"/>
        public Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string message = "")
        {
            return CommonHandleApplyAsync("newFriendRequestEvent", args, (int)action, message);
        }
        /// <summary>
        /// 异步处理加群请求
        /// </summary>
        /// <param name="args">收到用户入群申请事件中的参数, 即 <see cref="IGroupApplyEventArgs"/></param>
        /// <inheritdoc cref="CommonHandleApplyAsync"/>
        public Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string message = "")
        {
            return CommonHandleApplyAsync("memberJoinRequestEvent", args, (int)action, message);
        }
        /// <summary>
        /// 异步处理Bot受邀加群请求
        /// </summary>
        /// <param name="args">Bot受邀入群事件中的参数, 即 <see cref="IBotInvitedJoinGroupEventArgs"/></param>
        /// <inheritdoc cref="CommonHandleApplyAsync"/>
        public Task HandleBotInvitedJoinGroupAsync(IApplyResponseArgs args, GroupApplyActions action, string message = "")
        {
            return CommonHandleApplyAsync("botInvitedJoinGroupRequestEvent", args, (int)action, message);
        }
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        private Task CommonHandleApplyAsync(string actpath, IApplyResponseArgs args, int action, string message)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                eventId = args.EventId,
                fromId = args.FromQQ,
                groupId = args.FromGroup,
                operate = action,
                message
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/resp/{actpath}", payload, session.Token).AsApiRespAsync();
        }
    }
}
