using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberUnmutedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberUnmutedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供其它群成员被解除禁言事件相关信息的接口。继承自 <see cref="ISharedGroupMemberUnmutedEventArgs"/> 和 <see cref="IMemberOperatingEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberUnmuteEvent")]
    public interface IGroupMemberUnmutedEventArgs : ISharedGroupMemberUnmutedEventArgs, IMemberOperatingEventArgs
    {

    }

    public class GroupMemberUnmutedEventArgs : MemberOperatingEventArgs, IGroupMemberUnmutedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberUnmutedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberUnmutedEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {

        }
    }
}
