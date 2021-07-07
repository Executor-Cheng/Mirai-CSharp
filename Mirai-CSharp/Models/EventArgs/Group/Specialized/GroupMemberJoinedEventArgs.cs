using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberJoinedEventArgs : IMemberEventArgs
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
