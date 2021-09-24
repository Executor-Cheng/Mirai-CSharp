namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot在群里的权限被改变相关信息的接口。继承自 <see cref="IBotGroupPropertyChangedEventArgs{TRawdata, TProperty}"/>
    /// </summary>
    public interface IBotGroupPermissionChangedEventArgs : IBotGroupPropertyChangedEventArgs<GroupPermission>
    {

    }

    /// <summary>
    /// 提供Bot在群里的权限被改变相关信息的接口。继承自 <see cref="IBotGroupPermissionChangedEventArgs"/> 和 <see cref="IBotGroupPropertyChangedEventArgs{TRawdata, TProperty}"/>
    /// </summary>
    public interface IBotGroupPermissionChangedEventArgs<TRawdata> : IBotGroupPermissionChangedEventArgs, IBotGroupPropertyChangedEventArgs<TRawdata, GroupPermission>
    {

    }
}
