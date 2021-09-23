using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotPositiveLeaveGroupEventArgs = Mirai.CSharp.Models.EventArgs.IBotPositiveLeaveGroupEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动退出一个群相关信息的接口。继承自 <see cref="ISharedBotPositiveLeaveGroupEventArgs"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotLeaveEventActive")]
    public interface IBotPositiveLeaveGroupEventArgs : ISharedBotPositiveLeaveGroupEventArgs, IGroupEventArgs
    {

    }

    public class BotPositiveLeaveGroupEventArgs : GroupEventArgs, IBotPositiveLeaveGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveLeaveGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveLeaveGroupEventArgs(GroupInfo group) : base(group)
        {

        }
    }
}
