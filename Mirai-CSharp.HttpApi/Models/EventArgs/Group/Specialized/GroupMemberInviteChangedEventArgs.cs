using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberInviteChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberInviteChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群员邀请好友加群设置被改变相关信息的接口。继承自 <see cref="ISharedGroupMemberInviteChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupAllowMemberInviteEvent")]
    public interface IGroupMemberInviteChangedEventArgs : ISharedGroupMemberInviteChangedEventArgs, IGroupPropertyChangedEventArgs<bool>
    {

    }

    public class GroupMemberInviteChangedEventArgs : GroupPropertyChangedEventArgs<bool>, IGroupMemberInviteChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInviteChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInviteChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
        {

        }
    }
}
