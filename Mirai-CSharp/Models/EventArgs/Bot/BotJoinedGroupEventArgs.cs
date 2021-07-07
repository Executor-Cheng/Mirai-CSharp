using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotJoinedGroupEventArgs : IGroupEventArgs
    {

    }

    public class BotJoinedGroupEventArgs : GroupEventArgs, IBotJoinedGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs(GroupInfo group) : base(group)
        {

        }
    }
}
