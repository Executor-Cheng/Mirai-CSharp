using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被挤下线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotKickedOfflineEventArgs : IBotEventArgs
    {

    }

    public class BotKickedOfflineEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOfflineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOfflineEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
