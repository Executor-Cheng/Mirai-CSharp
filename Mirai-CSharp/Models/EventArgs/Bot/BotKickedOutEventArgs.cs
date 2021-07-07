using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotKickedOutEventArgs : IGroupEventArgs
    {

    }

    public class BotKickedOutEventArgs : GroupEventArgs, IBotKickedOutEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOutEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOutEventArgs(GroupInfo group) : base(group)
        {

        }
    }
}
