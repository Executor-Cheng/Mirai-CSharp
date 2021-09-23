using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotUnmutedEventArgs = Mirai.CSharp.Models.EventArgs.IBotUnmutedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被解除禁言事件相关信息的接口。继承自 <see cref="ISharedBotUnmutedEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotUnmuteEvent")]
    public interface IBotUnmutedEventArgs : ISharedBotUnmutedEventArgs, IOperatorEventArgs
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
