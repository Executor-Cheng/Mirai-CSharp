using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群名称改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    /// 
    public interface IGroupNameChangedEventArgs : IGroupPropertyChangedEventArgs<string>
    {

    }

    public class GroupNameChangedEventArgs : GroupPropertyChangedEventArgs<string>, IGroupNameChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupNameChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupNameChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
        {

        }
    }
}
