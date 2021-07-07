using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供成员被踢出群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberKickedEventArgs : IMemberOperatingEventArgs
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
