using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberJoinedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberJoinedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="ISharedGroupMemberJoinedEventArgs"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberJoinEvent")]
    public interface IGroupMemberJoinedEventArgs : ISharedGroupMemberJoinedEventArgs, IMemberEventArgs
    {

    }

    public class GroupMemberJoinedEventArgs : MemberEventArgs, IGroupMemberJoinedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberJoinedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberJoinedEventArgs(IGroupMemberInfo member) : base(member)
        {

        }
    }
}
