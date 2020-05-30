using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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
            CheckConnected();
            return InternalHttpGetAsync<IFriendInfo[], FriendInfo[]>($"{SessionInfo.Options.BaseUrl}/friendList?sessionKey={SessionInfo.SessionKey}", SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步获取群列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public Task<IGroupInfo[]> GetGroupListAsync()
        {
            CheckConnected();
            return InternalHttpGetAsync<IGroupInfo[], GroupInfo[]>($"{SessionInfo.Options.BaseUrl}/groupList?sessionKey={SessionInfo.SessionKey}", SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步获取群成员列表
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要进行查询的群号</param>
        public Task<IGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber)
        {
            CheckConnected();
            return InternalHttpGetAsync<IGroupMemberInfo[], GroupMemberInfo[]>($"{SessionInfo.Options.BaseUrl}/memberList?sessionKey={SessionInfo.SessionKey}&target={groupNumber}", SessionInfo.Canceller.Token);
        }

        private Task InternalToggleMuteAllAsync(bool action, long groupNumber)
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/{(action ? "muteAll" : "unmuteAll")}", payload, SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步开启全体禁言
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要进行全体禁言的群号</param>
        public Task MuteAllAsync(long groupNumber)
        {
            return InternalToggleMuteAllAsync(true, groupNumber);
        }
        /// <summary>
        /// 异步关闭全体禁言
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="PermissionDeniedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要关闭全体禁言的群号</param>
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
            CheckConnected();
            if (duration <= TimeSpan.Zero || duration >= TimeSpan.FromDays(30))
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
                memberId,
                time = (int)duration.TotalSeconds
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/mute", payload, SessionInfo.Canceller.Token);
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
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
                memberId,
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/unmute", payload, SessionInfo.Canceller.Token);
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
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
                memberId,
                msg
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/kick", payload, SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步使当前机器人退出给定的群
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">将要退出的群号</param>
        public Task LeaveGroupAsync(long groupNumber)
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/quit", payload, SessionInfo.Canceller.Token);
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
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
                config
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/groupConfig", payload, SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步获取群信息
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">要获取信息的群号</param>
        public Task<IGroupConfig> GetGroupConfigAsync(long groupNumber)
        {
            CheckConnected();
            return InternalHttpGetAsync<IGroupConfig, GroupConfig>($"{SessionInfo.Options.BaseUrl}/groupConfig?sessionKey={SessionInfo.SessionKey}&target={groupNumber}", SessionInfo.Canceller.Token);
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
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = groupNumber,
                memberId,
                info
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/memberInfo", payload, SessionInfo.Canceller.Token);
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
            CheckConnected();
            return InternalHttpGetAsync<IGroupMemberCardInfo, GroupMemberCardInfo>($"{SessionInfo.Options.BaseUrl}/memberInfo?sessionKey={SessionInfo.SessionKey}&target={groupNumber}&memberId={memberId}", SessionInfo.Canceller.Token);
        }
    }
}
