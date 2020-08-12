using Mirai_CSharp.Models;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 收到群消息
        /// </summary>
        public event CommonEventHandler<IGroupMessageEventArgs>? GroupMessageEvt;
        /// <summary>
        /// 群消息被撤回
        /// </summary>
        public event CommonEventHandler<IGroupMessageRevokedEventArgs>? GroupMessageRevokedEvt;
        /// <summary>
        /// 某个群名被改变
        /// </summary>
        public event CommonEventHandler<IGroupNameChangedEventArgs>? GroupNameChangedEvt;
        /// <summary>
        /// 某群入群公告改变
        /// </summary>
        public event CommonEventHandler<IGroupEntranceAnnouncementChangedEventArgs>? GroupEntranceAnnouncementChangedEvt;
        /// <summary>
        /// 全员禁言设置被改变
        /// </summary>
        public event CommonEventHandler<IGroupMuteAllChangedEventArgs>? GroupMuteAllChangedEvt;
        /// <summary>
        /// 匿名聊天设置被改变
        /// </summary>
        public event CommonEventHandler<IGroupAnonymousChatChangedEventArgs>? GroupAnonymousChatChangedEvt;
        /// <summary>
        /// 坦白说设置被改变
        /// </summary>
        public event CommonEventHandler<IGroupConfessTalkChangedEventArgs>? GroupConfessTalkChangedEvt;
        /// <summary>
        /// 群员邀请好友加群设置被改变
        /// </summary>
        public event CommonEventHandler<IGroupMemberInviteChangedEventArgs>? GroupMemberInviteChangedEvt;
        /// <summary>
        /// 新人入群
        /// </summary>
        public event CommonEventHandler<IGroupMemberJoinedEventArgs>? GroupMemberJoinedEvt;
        /// <summary>
        /// 成员主动离群（该成员不是Bot, 见 <see cref="BotPositiveLeaveGroupEvt"/>）
        /// </summary>
        public event CommonEventHandler<IGroupMemberPositiveLeaveEventArgs>? GroupMemberPositiveLeaveEvt;
        /// <summary>
        /// 成员被踢出群（该成员不是Bot, 见 <see cref="BotKickedOutEvt"/>）
        /// </summary>
        public event CommonEventHandler<IGroupMemberKickedEventArgs>? GroupMemberKickedEvt;
        /// <summary>
        /// 群名片改动
        /// </summary>
        public event CommonEventHandler<IGroupMemberCardChangedEventArgs>? GroupMemberCardChangedEvt;
        /// <summary>
        /// 群头衔改动（只有群主有操作限权）
        /// </summary>
        public event CommonEventHandler<IGroupMemberSpecialTitleChangedEventArgs>? GroupMemberSpecialTitleChangedEvt;
        /// <summary>
        /// 成员权限改变（该成员不是Bot, 见 <see cref="BotGroupPermissionChangedEvt"/>）
        /// </summary>
        public event CommonEventHandler<IGroupMemberPermissionChangedEventArgs>? GroupMemberPermissionChangedEvt;
        /// <summary>
        /// 群成员被禁言（该成员不可能是Bot, 见 <see cref="BotMutedEventArgs"/>）
        /// </summary>
        public event CommonEventHandler<IGroupMemberMutedEventArgs>? GroupMemberMutedEvt;
        /// <summary>
        /// 群成员被取消禁言（该成员不可能是Bot, 见 <see cref="BotUnmutedEventArgs"/>）
        /// </summary>
        public event CommonEventHandler<IGroupMemberUnmutedEventArgs>? GroupMemberUnmutedEvt;
        /// <summary>
        /// 收到用户入群申请（Bot需要有 <see cref="GroupPermission.Administrator"/> 或 <see cref="GroupPermission.Owner"/> 权限）
        /// </summary>
        public event CommonEventHandler<IGroupApplyEventArgs>? GroupApplyEvt;
    }
}
