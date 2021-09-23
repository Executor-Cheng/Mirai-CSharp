using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotKickedOutEventArgs = Mirai.CSharp.Models.EventArgs.IBotKickedOutEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="ISharedBotKickedOutEventArgs"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotLeaveEventKick")]
    public interface IBotKickedOutEventArgs : ISharedBotKickedOutEventArgs, IGroupEventArgs
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
