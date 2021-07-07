using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群头衔改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberSpecialTitleChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    public class GroupMemberSpecialTitleChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>, IGroupMemberSpecialTitleChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberSpecialTitleChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberSpecialTitleChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
