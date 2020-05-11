using Mirai_CSharp.Models;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// Bot登录成功
        /// </summary>
        public event CommonEventHandler<long> BotOnlineEvt;
        /// <summary>
        /// Bot主动离线
        /// </summary>
        public event CommonEventHandler<long> BotPositiveOfflineEvt;
        /// <summary>
        /// Bot被挤下线
        /// </summary>
        public event CommonEventHandler<long> BotKickedOfflineEvt;
        /// <summary>
        /// Bot意外断开连接(服务器主动断开连接、网络问题等等)
        /// </summary>
        public event CommonEventHandler<long> BotDroppedEvt;
        /// <summary>
        /// Bot主动重新登录
        /// </summary>
        public event CommonEventHandler<long> BotReloginEvt;
        /// <summary>
        /// Bot在群里的权限被改变. 操作人一定是群主
        /// </summary>
        public event CommonEventHandler<IBotGroupPropertyChangedEventArgs<GroupPermission>> BotGroupPermissionChangedEvt;
        /// <summary>
        /// Bot被禁言
        /// </summary>
        public event CommonEventHandler<IBotMutedEventArgs> BotMutedEvt;
        /// <summary>
        /// Bot被取消禁言
        /// </summary>
        public event CommonEventHandler<IBotUnmutedEventArgs> BotUnmutedEvt;
        /// <summary>
        /// Bot加入了一个新群
        /// </summary>
        public event CommonEventHandler<IGroupEventArgs> BotJoinedGroupEvt;
        /// <summary>
        /// Bot主动退出一个群
        /// </summary>
        public event CommonEventHandler<IGroupEventArgs> BotPositiveLeaveGroupEvt;
        /// <summary>
        /// Bot被踢出一个群
        /// </summary>
        public event CommonEventHandler<IGroupEventArgs> BotKickedOutEvt;
    }
}
