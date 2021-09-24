using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotDroppedEventArgs = Mirai.CSharp.Models.EventArgs.IBotDroppedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot意外断开连接信息的接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IBotDroppedEventArgs{TRawdata}"/> 和 <see cref="IBotEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotOfflineEventDropped")]
    public interface IBotDroppedEventArgs : ISharedBotDroppedEventArgs, IBotEventArgs
    {

    }

    public class BotDroppedEventArgs : BotEventArgs, IBotDroppedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotDroppedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotDroppedEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
