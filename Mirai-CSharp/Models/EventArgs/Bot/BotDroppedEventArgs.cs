using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot意外断开连接信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotDroppedEventArgs : IBotEventArgs
    {

    }

    public class BotDroppedEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotDroppedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotDroppedEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
