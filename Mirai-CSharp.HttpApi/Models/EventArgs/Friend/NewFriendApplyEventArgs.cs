using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedNewFriendApplyEventArgs = Mirai.CSharp.Models.EventArgs.INewFriendApplyEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友申请相关信息的接口。继承自 <see cref="ISharedNewFriendApplyEventArgs"/> 和 <see cref="INewApplyEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("NewFriendRequestEvent")]
    public interface INewFriendApplyEventArgs : ISharedNewFriendApplyEventArgs, INewApplyEventArgs
    {

    }

    public class NewFriendApplyEventArgs : NewApplyEventArgs, INewFriendApplyEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public NewFriendApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public NewFriendApplyEventArgs(long eventId, long fromGroup, long fromQQ, string nickName, string message) : base(eventId, fromGroup, fromQQ, nickName, message)
        {

        }
    }
}
