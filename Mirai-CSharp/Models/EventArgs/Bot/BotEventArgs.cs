using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 机器人本身事件接口。仅含QQ号
    /// </summary>
    public interface IBotEventArgs : IEventArgsBase
    {
        /// <summary>
        /// 机器人QQ号
        /// </summary>
        long QQNumber { get; }
    }

    public abstract class BotEventArgs : EventArgsBase, IBotEventArgs
    {
        /// <summary>
        /// 机器人QQ号
        /// </summary>
        public long QQNumber { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected BotEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected BotEventArgs(long qqNumber)
        {
            QQNumber = qqNumber;
        }
    }
}
