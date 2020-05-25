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
        public GroupMemberStringPropertyChangedEventArgs()
        {

        }

        public GroupMemberStringPropertyChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
        {

        }
    }
}
