using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotPositiveOfflineEventArgs = Mirai.CSharp.Models.EventArgs.IBotPositiveOfflineEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动离线信息的接口。继承自 <see cref="ISharedBotPositiveOfflineEventArgs"/> 和 <see cref="IBotEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotOfflineEventActive")]
    public interface IBotPositiveOfflineEventArgs : ISharedBotPositiveOfflineEventArgs, IBotEventArgs
    {

    }

    public class BotPositiveOfflineEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveOfflineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotPositiveOfflineEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
