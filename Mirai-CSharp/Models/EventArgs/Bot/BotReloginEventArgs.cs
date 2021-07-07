using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动重新登录信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotReloginEventArgs : IBotEventArgs
    {

    }

    public class BotReloginEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotReloginEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotReloginEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
