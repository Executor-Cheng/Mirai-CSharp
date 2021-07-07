using Mirai_CSharp.Models;
using Mirai_CSharp.Models.EventArgs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步处理Bot受邀加群请求
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="args">Bot受邀入群事件中的参数, 即 <see cref="IBotInvitedJoinGroupEventArgs"/></param>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task HandleBotInvitedJoinGroupAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        /// <summary>
        /// 异步处理加群请求
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="args">收到用户入群申请事件中的参数, 即 <see cref="IGroupApplyEventArgs"/></param>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task HandleGroupApplyAsync(IApplyResponseArgs args, GroupApplyActions action, string? message = null, CancellationToken token = default);

        /// <summary>
        /// 异步处理添加好友请求
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="args">收到添加好友申请事件中的参数, 即<see cref="INewFriendApplyEventArgs"/></param>
        /// <param name="action">处理方式</param>
        /// <param name="message">附加信息</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task HandleNewFriendApplyAsync(IApplyResponseArgs args, FriendApplyAction action, string? message = null, CancellationToken token = default);
    }
}
