namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供Bot在群里的权限被改变相关信息的接口。继承自 <see cref="IBotGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IBotGroupPermissionChangedEventArgs : IBotGroupPropertyChangedEventArgs<GroupPermission>
    {

    }

    public class BotGroupPermissionChangedEventArgs : BotGroupEnumPropertyChangedEventArgs<GroupPermission>,
                                                      IBotGroupPermissionChangedEventArgs
    {
        public BotGroupPermissionChangedEventArgs()
        {

        }

        public BotGroupPermissionChangedEventArgs(IGroupInfo group, GroupPermission origin, GroupPermission current) : base(group, origin, current)
        {

        }
    }
}
