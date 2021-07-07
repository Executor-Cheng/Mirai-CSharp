using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供成员主动离群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberPositiveLeaveEventArgs : IMemberEventArgs
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
