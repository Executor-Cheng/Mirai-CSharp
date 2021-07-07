using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动离线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotPositiveOfflineEventArgs : IBotEventArgs
    {

    }

    public class BotPositiveOfflineEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveOfflineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveOfflineEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
