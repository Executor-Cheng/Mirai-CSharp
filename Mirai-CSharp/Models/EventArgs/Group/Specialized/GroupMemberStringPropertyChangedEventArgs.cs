using System;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群名片改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberCardChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供群头衔改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberSpecialTitleChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    public class GroupMemberStringPropertyChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>,
                                                             IGroupMemberCardChangedEventArgs,
                                                             IGroupMemberSpecialTitleChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberStringPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberStringPropertyChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
