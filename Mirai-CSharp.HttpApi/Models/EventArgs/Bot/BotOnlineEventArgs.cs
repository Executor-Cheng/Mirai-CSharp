using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotOnlineEventArgs = Mirai.CSharp.Models.EventArgs.IBotOnlineEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot登录成功信息的接口。继承自 <see cref="ISharedBotOnlineEventArgs"/> 和 <see cref="IBotEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotOnlineEvent")]
    public interface IBotOnlineEventArgs : ISharedBotOnlineEventArgs, IBotEventArgs
    {

    }

    public class BotOnlineEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotOnlineEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotOnlineEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
