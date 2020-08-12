using System;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供全员禁言设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMuteAllChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供匿名聊天设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupAnonymousChatChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供坦白说设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupConfessTalkChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供群员邀请好友加群设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    public interface IGroupMemberInviteChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    public class GroupBoolPropertyChangedEventArgs : GroupPropertyChangedEventArgs<bool>, 
                                                     IGroupMuteAllChangedEventArgs, 
                                                     IGroupAnonymousChatChangedEventArgs,
                                                     IGroupConfessTalkChangedEventArgs,
                                                     IGroupMemberInviteChangedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupBoolPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupBoolPropertyChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
        {

        }
    }
}
