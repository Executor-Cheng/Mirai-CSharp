using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步获取好友列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task<IFriendInfo[]> GetFriendListAsync(CancellationToken token = default);

        /// <summary>
        /// 异步获取群信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">要获取信息的群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task<IGroupConfig> GetGroupConfigAsync(long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步获取群列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task<IGroupInfo[]> GetGroupListAsync(CancellationToken token = default);

        /// <summary>
        /// 异步开启全体禁言
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要进行全体禁言的群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task MuteAllAsync(long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步禁言给定用户
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要被禁言的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="duration">禁言时长。必须介于[1秒, 30天]</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task MuteAsync(long memberId, long groupNumber, TimeSpan duration, CancellationToken token = default);

        /// <summary>
        /// 异步关闭全体禁言
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要关闭全体禁言的群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task UnmuteAllAsync(long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步解禁给定用户
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要解除禁言的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task UnmuteAsync(long memberId, long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步将给定用户踢出给定的群
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要被踢出的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="msg">附加消息</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task KickMemberAsync(long memberId, long groupNumber, string msg = "您已被移出群聊", CancellationToken token = default);

        /// <summary>
        /// 异步使当前机器人退出给定的群
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要退出的群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task LeaveGroupAsync(long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步修改群信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">要进行修改的群号</param>
        /// <param name="config">群信息。其中不进行修改的值请置为 <see langword="null"/></param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task ChangeGroupConfigAsync(long groupNumber, IGroupConfig config, CancellationToken token = default);

        /// <summary>
        /// 异步修改给定群员的信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要修改信息的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="info">用户信息。其中不进行修改的值请置为 <see langword="null"/></param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task ChangeGroupMemberInfoAsync(long memberId, long groupNumber, IGroupMemberCardInfo info, CancellationToken token = default);

        /// <summary>
        /// 异步获取给定群员的信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">要获取信息的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task<IGroupMemberCardInfo> GetGroupMemberInfoAsync(long memberId, long groupNumber, CancellationToken token = default);

        /// <summary>
        /// 异步获取群成员列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要进行查询的群号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task<IGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber, CancellationToken token = default);
    }
}
