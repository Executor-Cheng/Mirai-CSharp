using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot登录成功信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotOnlineEventArgs : IBotEventArgs
    {

    }

    public class BotOnlineEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotOnlineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotOnlineEventArgs(long qqNumber) : base(qqNumber)
        {
            
        }
    }
}
