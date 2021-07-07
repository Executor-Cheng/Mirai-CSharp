using System;

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
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupPermissionChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupPermissionChangedEventArgs(IGroupInfo group, GroupPermission origin, GroupPermission current) : base(group, origin, current)
        {

        }
    }
}
