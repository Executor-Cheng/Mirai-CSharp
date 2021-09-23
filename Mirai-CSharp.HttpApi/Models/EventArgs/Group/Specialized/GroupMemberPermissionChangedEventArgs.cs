using System;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.Models;
using ISharedGroupMemberPermissionChangedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberPermissionChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供成员权限改变相关信息的接口。继承自 <see cref="ISharedGroupMemberPermissionChangedEventArgs"/> 和 <see cref="IGroupMemberEnumPropertyChangedEventArgs{GroupPermission}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberPermissionChangeEvent")]
    public interface IGroupMemberPermissionChangedEventArgs : ISharedGroupMemberPermissionChangedEventArgs, IGroupMemberEnumPropertyChangedEventArgs<GroupPermission>
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
