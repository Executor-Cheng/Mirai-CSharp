using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 异步获取好友列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public Task<IFriendInfo[]> GetFriendListAsync()
        {
            InternalSessionInfo session = SafeGetSession();
            return session.Client.GetAsync($"{session.Options.BaseUrl}/friendList?sessionKey={session.SessionKey}", session.Token)
                .AsApiRespAsync<IFriendInfo[], FriendInfo[]>(session.Token);
        }
        /// <summary>
        /// 异步获取群列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public Task<IGroupInfo[]> GetGroupListAsync()
        {
            InternalSessionInfo session = SafeGetSession();
            return session.Client.GetAsync($"{session.Options.BaseUrl}/groupList?sessionKey={session.SessionKey}", session.Token)
                .AsApiRespAsync<IGroupInfo[], GroupInfo[]>(session.Token);
        }
        /// <summary>
        /// 异步获取群成员列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要进行查询的群号</param>
        public Task<IGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            return session.Client.GetAsync($"{session.Options.BaseUrl}/memberList?sessionKey={session.SessionKey}&target={groupNumber}", session.Token)
                .AsApiRespAsync<IGroupMemberInfo[], GroupMemberInfo[]>(session.Token);
        }
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="action">禁言为 <see langword="true"/>, 解禁为 <see langword="false"/></param>
        /// <param name="groupNumber">将要操作的群号</param>
        private Task InternalToggleMuteAllAsync(bool action, long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/{(action ? "muteAll" : "unmuteAll")}", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步开启全体禁言
        /// </summary>
        /// <param name="groupNumber">将要进行全体禁言的群号</param>
        /// <inheritdoc cref="InternalToggleMuteAllAsync"/>
        public Task MuteAllAsync(long groupNumber)
        {
            return InternalToggleMuteAllAsync(true, groupNumber);
        }
        /// <summary>
        /// 异步关闭全体禁言
        /// </summary>
        /// <param name="groupNumber">将要关闭全体禁言的群号</param>
        /// <inheritdoc cref="InternalToggleMuteAllAsync"/>
        public Task UnmuteAllAsync(long groupNumber)
        {
            return InternalToggleMuteAllAsync(false, groupNumber);
        }
        /// <summary>
        /// 异步禁言给定用户
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要被禁言的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="duration">禁言时长。必须介于[1秒, 30天]</param>
        public Task MuteAsync(long memberId, long groupNumber, TimeSpan duration)
        {
            InternalSessionInfo session = SafeGetSession();
            if (duration <= TimeSpan.Zero || duration >= TimeSpan.FromDays(30))
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                time = (int)duration.TotalSeconds
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/mute", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步解禁给定用户
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要解除禁言的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        public Task UnmuteAsync(long memberId, long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/unmute", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步将给定用户踢出给定的群
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要被踢出的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="msg">附加消息</param>
        public Task KickMemberAsync(long memberId, long groupNumber, string msg = "您已被移出群聊")
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                msg
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/kick", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步使当前机器人退出给定的群
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要退出的群号</param>
        public Task LeaveGroupAsync(long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/quit", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步修改群信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">要进行修改的群号</param>
        /// <param name="config">群信息。其中不进行修改的值请置为 <see langword="null"/></param>
        public Task ChangeGroupConfigAsync(long groupNumber, IGroupConfig config)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                config
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/groupConfig", payload, JsonSerializeOptionsFactory.IgnoreNulls, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步获取群信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">要获取信息的群号</param>
        public Task<IGroupConfig> GetGroupConfigAsync(long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            return session.Client.GetAsync($"{session.Options.BaseUrl}/groupConfig?sessionKey={session.SessionKey}&target={groupNumber}", session.Token)
                .AsApiRespAsync<IGroupConfig, GroupConfig>(session.Token);
        }
        /// <summary>
        /// 异步修改给定群员的信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">将要修改信息的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        /// <param name="info">用户信息。其中不进行修改的值请置为 <see langword="null"/></param>
        public Task ChangeGroupMemberInfoAsync(long memberId, long groupNumber, IGroupMemberCardInfo info)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                info
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/memberInfo", payload, session.Token)
                .AsApiRespAsync(session.Token);
        }
        /// <summary>
        /// 异步获取给定群员的信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="memberId">要获取信息的QQ号</param>
        /// <param name="groupNumber">该用户所在群号</param>
        public Task<IGroupMemberCardInfo> GetGroupMemberInfoAsync(long memberId, long groupNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            return session.Client.GetAsync($"{session.Options.BaseUrl}/memberInfo?sessionKey={session.SessionKey}&target={groupNumber}&memberId={memberId}", session.Token)
                .AsApiRespAsync<IGroupMemberCardInfo, GroupMemberCardInfo>(session.Token);
        }
    }
}
