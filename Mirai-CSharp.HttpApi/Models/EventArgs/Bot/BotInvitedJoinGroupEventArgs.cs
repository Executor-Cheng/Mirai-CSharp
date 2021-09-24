using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedBotInvitedJoinGroupEventArgs = Mirai.CSharp.Models.EventArgs.IBotInvitedJoinGroupEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被邀请入群相关信息的接口。继承自 <see cref="ISharedBotInvitedJoinGroupEventArgs"/> 和 <see cref="ICommonGroupApplyEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotInvitedJoinGroupRequestEvent")]
    public interface IBotInvitedJoinGroupEventArgs : ISharedBotInvitedJoinGroupEventArgs, ICommonGroupApplyEventArgs
    {

    }

    public class BotInvitedJoinGroupEventArgs : CommonGroupApplyEventArgs, IBotInvitedJoinGroupEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotInvitedJoinGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotInvitedJoinGroupEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName, string message) : base(fromGroupName, eventId, fromGroup, fromQQ, nickName, message)
        {

        }
    }
}
