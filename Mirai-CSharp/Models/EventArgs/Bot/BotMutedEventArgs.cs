using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/> 和 <see cref="IMutedEventArgs"/>
    /// </summary>
    public interface IBotMutedEventArgs : IOperatorEventArgs, IMutedEventArgs
    {

    }

    public class BotMutedEventArgs : OperatorEventArgs, IBotMutedEventArgs
    {
        public TimeSpan Duration { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotMutedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotMutedEventArgs(TimeSpan duration, GroupMemberInfo @operator) : base(@operator)
        {
            Duration = duration;
        }
    }
}
