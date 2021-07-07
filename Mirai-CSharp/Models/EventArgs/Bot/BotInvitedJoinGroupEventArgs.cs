using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被邀请入群相关信息的接口。继承自 <see cref="ICommonGroupApplyEventArgs"/>
    /// </summary>
    public interface IBotInvitedJoinGroupEventArgs : ICommonGroupApplyEventArgs
    {

    }

    public class BotInvitedJoinGroupEventArgs : CommonGroupApplyEventArgs, IBotInvitedJoinGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotInvitedJoinGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotInvitedJoinGroupEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName) : base(fromGroupName, eventId, fromGroup, fromQQ, nickName)
        {

        }
    }
}
