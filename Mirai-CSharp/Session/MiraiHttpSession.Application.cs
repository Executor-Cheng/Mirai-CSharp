using Mirai_CSharp.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 异步处理添加好友请求
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="args">收到添加好友申请事件中的参数</param>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        public Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string message = "")
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                eventId = args.EventId,
                fromId = args.FromQQ,
                groupId = args.FromGroup,
                operate = (int)action,
                message
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/resp/newFriendRequestEvent", payload, SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步处理加群请求或Bot受邀入群请求
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="args">请提供以下之一:
        /// <list type="bullet">
        /// <item>收到用户入群申请事件中的参数, 即 <see cref="IGroupApplyEventArgs"/></item>
        /// <item>Bot受邀入群事件中的参数, 即 <see cref="IBotInvitedJoinGroupEventArgs"/></item>
        /// </list>
        /// </param>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        public Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string message = "")
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                eventId = args.EventId,
                fromId = args.FromQQ,
                groupId = args.FromGroup,
                operate = (int)action,
                message
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/resp/memberJoinRequestEvent", payload, SessionInfo.Canceller.Token);
        }
    }
}
