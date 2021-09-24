using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotReloginEventArgs = Mirai.CSharp.Models.EventArgs.IBotReloginEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动重新登录信息的接口。继承自 <see cref="ISharedBotReloginEventArgs"/> 和 <see cref="IBotEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotReloginEvent")]
    public interface IBotReloginEventArgs : ISharedBotReloginEventArgs, IBotEventArgs
    {

    }

    public class BotReloginEventArgs : BotEventArgs, IBotOnlineEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotReloginEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotReloginEventArgs(long qqNumber) : base(qqNumber)
        {

        }
    }
}
