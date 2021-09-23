using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupApplyEventArgs = Mirai.CSharp.Models.EventArgs.IGroupApplyEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请相关信息的接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IGroupApplyEventArgs{TRawdata}"/> 和 <see cref="ICommonGroupApplyEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberJoinRequestEvent")]
    public interface IGroupApplyEventArgs : ISharedGroupApplyEventArgs, ICommonGroupApplyEventArgs
    {

    }

    public class GroupApplyEventArgs : CommonGroupApplyEventArgs, IGroupApplyEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName, string message) : base(fromGroupName, eventId, fromGroup, fromQQ, nickName, message)
        {

        }
    }
}
