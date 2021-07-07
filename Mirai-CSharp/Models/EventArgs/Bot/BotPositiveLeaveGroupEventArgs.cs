using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动退出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotPositiveLeaveGroupEventArgs : IGroupEventArgs
    {

    }

    public class BotPositiveLeaveGroupEventArgs : GroupEventArgs, IBotPositiveLeaveGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveLeaveGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveLeaveGroupEventArgs(GroupInfo group) : base(group)
        {

        }
    }
}
