using System;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群名称改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    /// 
    public interface IGroupNameChangedEventArgs : IGroupPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供入群公告改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupEntranceAnnouncementChangedEventArgs : IGroupPropertyChangedEventArgs<string>
    {

    }

    public class GroupStringPropertyChangedEventArgs : GroupPropertyChangedEventArgs<string>, 
                                                       IGroupNameChangedEventArgs, 
                                                       IGroupEntranceAnnouncementChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupStringPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupStringPropertyChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
        {

        }
    }
}
