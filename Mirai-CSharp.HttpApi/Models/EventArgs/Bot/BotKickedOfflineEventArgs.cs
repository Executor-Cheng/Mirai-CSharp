using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotKickedOfflineEventArgs = Mirai.CSharp.Models.EventArgs.IBotKickedOfflineEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被挤下线信息的接口。继承自 <see cref="ISharedBotKickedOfflineEventArgs"/> 和 <see cref="IBotEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotOfflineEventForce")]
    public interface IBotKickedOfflineEventArgs : ISharedBotKickedOfflineEventArgs, IBotEventArgs
    {

    }

    public class BotKickedOfflineEventArgs : BotEventArgs, IBotKickedOfflineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOfflineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotKickedOfflineEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
