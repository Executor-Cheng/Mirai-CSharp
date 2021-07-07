using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供禁言信息的接口
    /// </summary>
    public interface IMutedEventArgs
    {
        TimeSpan Duration { get; }
    }

    /// <summary>
    /// 提供其它群成员被解除禁言事件相关信息的接口。继承自 <see cref="IMemberOperatingEventArgs"/>
    /// </summary>
    public interface IGroupMemberUnmutedEventArgs : IMemberOperatingEventArgs
    {

    }

    public class GroupMemberUnmutedEventArgs : MemberOperatingEventArgs, IGroupMemberUnmutedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberUnmutedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberUnmutedEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {

        }
    }

    /// <summary>
    /// 提供其它群成员被禁言事件相关信息的接口。继承自 <see cref="IMemberOperatingEventArgs"/> 和 <see cref="IMutedEventArgs"/>
    /// </summary>
    public interface IGroupMemberMutedEventArgs : IMemberOperatingEventArgs, IMutedEventArgs 
    {
    
    }

    public class GroupMemberMutedEventArgs : MemberOperatingEventArgs, IGroupMemberMutedEventArgs
    {
        public TimeSpan Duration { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberMutedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberMutedEventArgs(TimeSpan duration, IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {
            Duration = duration;
        }
    }
}
