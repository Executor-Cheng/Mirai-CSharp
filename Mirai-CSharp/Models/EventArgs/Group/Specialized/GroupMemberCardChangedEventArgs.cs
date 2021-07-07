using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群名片改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberCardChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    public class GroupMemberCardChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>, IGroupMemberCardChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
