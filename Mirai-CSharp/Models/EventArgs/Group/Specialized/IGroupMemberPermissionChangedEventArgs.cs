namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供成员权限改变相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{GroupPermission}"/>
    /// </summary>
    public interface IGroupMemberPermissionChangedEventArgs : IGroupMemberPropertyChangedEventArgs<GroupPermission>
    {

    }

    /// <summary>
    /// 提供成员权限改变相关信息的接口。继承自 <see cref="IGroupMemberPermissionChangedEventArgs"/> 和 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata, TProperty}"/>
    /// </summary>
    public interface IGroupMemberPermissionChangedEventArgs<TRawdata> : IGroupMemberPermissionChangedEventArgs, IGroupMemberPropertyChangedEventArgs<TRawdata, GroupPermission>
    {

    }
}
