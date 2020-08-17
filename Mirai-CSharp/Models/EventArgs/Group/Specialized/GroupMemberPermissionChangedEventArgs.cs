using System;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供成员权限改变相关信息的接口。继承自 <see cref="IGroupMemberEnumPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberPermissionChangedEventArgs : IGroupMemberEnumPropertyChangedEventArgs<GroupPermission>
    {

    }

    public class GroupMemberPermissionChangedEventArgs : GroupMemberEnumPropertyChangedEventArgs<GroupPermission>,
                                                         IGroupMemberPermissionChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPermissionChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPermissionChangedEventArgs(IGroupMemberInfo member, GroupPermission origin, GroupPermission current) : base(member, origin, current)
        {

        }
    }
}
