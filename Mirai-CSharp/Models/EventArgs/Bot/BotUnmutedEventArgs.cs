using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被解除禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IBotUnmutedEventArgs : IOperatorEventArgs
    {

    }

    public class BotUnmutedEventArgs : OperatorEventArgs, IBotUnmutedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotUnmutedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotUnmutedEventArgs(GroupMemberInfo @operator) : base(@operator)
        {

        }
    }
}
