using Mirai_CSharp.Models;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 收到好友消息
        /// </summary>
        public event CommonEventHandler<IFriendMessageEventArgs>? FriendMessageEvt;
        /// <summary>
        /// 好友消息被撤回
        /// </summary>
        public event CommonEventHandler<IFriendMessageRevokedEventArgs>? FriendMessageRevokedEvt;
        /// <summary>
        /// 收到添加好友申请
        /// </summary>
        public event CommonEventHandler<INewFriendApplyEventArgs>? NewFriendApplyEvt;
    }
}
