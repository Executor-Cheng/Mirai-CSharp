using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedGroupMemberPositiveLeaveEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberPositiveLeaveEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供成员主动离群相关信息的接口。继承自 <see cref="ISharedGroupMemberPositiveLeaveEventArgs"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberLeaveEventQuit")]
    public interface IGroupMemberPositiveLeaveEventArgs : ISharedGroupMemberPositiveLeaveEventArgs, IMemberEventArgs
    {

    }

    public class GroupMemberPositiveLeaveEventArgs : MemberEventArgs, IGroupMemberPositiveLeaveEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPositiveLeaveEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPositiveLeaveEventArgs(IGroupMemberInfo member) : base(member)
        {

        }
    }
}
