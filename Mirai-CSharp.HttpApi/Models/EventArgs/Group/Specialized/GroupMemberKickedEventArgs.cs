using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberKickedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberKickedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供成员被踢出群相关信息的接口。继承自 <see cref="ISharedGroupMemberKickedEventArgs"/> 和 <see cref="IMemberOperatingEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberLeaveEventKick")]
    public interface IGroupMemberKickedEventArgs : ISharedGroupMemberKickedEventArgs, IMemberOperatingEventArgs
    {

    }

    public class GroupMemberKickedEventArgs : MemberOperatingEventArgs, IGroupMemberKickedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberKickedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberKickedEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {

        }
    }
}
