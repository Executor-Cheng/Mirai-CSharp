using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotJoinedGroupEventArgs = Mirai.CSharp.Models.EventArgs.IBotJoinedGroupEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="ISharedBotJoinedGroupEventArgs"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotJoinGroupEvent")]
    public interface IBotJoinedGroupEventArgs : ISharedBotJoinedGroupEventArgs, IGroupEventArgs
    {

    }

    public class BotJoinedGroupEventArgs : GroupEventArgs, IBotJoinedGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs(GroupInfo group) : base(group)
        {

        }
    }
}
