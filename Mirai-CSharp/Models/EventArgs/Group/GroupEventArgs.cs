using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群事件的信息接口
    /// </summary>
    public interface IGroupEventArgs : IEventArgsBase
    {
        /// <summary>
        /// 来源群信息
        /// </summary>
        IGroupInfo Group { get; }
    }

    public abstract class GroupEventArgs : EventArgsBase, IGroupEventArgs
    {
        /// <inheritdoc/>
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected GroupEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected GroupEventArgs(GroupInfo group)
        {
            Group = group;
        }
    }

    /// <summary>
    /// 提供群成员信息的接口
    /// </summary>
    public interface IMemberEventArgs : IEventArgsBase
    {
        IGroupMemberInfo Member { get; }
    }

    public abstract class MemberEventArgs : EventArgsBase, IMemberEventArgs
    {
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberEventArgs(IGroupMemberInfo member)
        {
            Member = member;
        }
    }

    /// <summary>
    /// 提供群内操作者信息的接口
    /// </summary>
    public interface IOperatorEventArgs : IEventArgsBase
    {
        IGroupMemberInfo Operator { get; }
    }

    public abstract class OperatorEventArgs : EventArgsBase, IOperatorEventArgs
    {
        public virtual IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected OperatorEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected OperatorEventArgs(IGroupMemberInfo @operator)
        {
            Operator = @operator;
        }
    }

    /// <summary>
    /// 提供群内管理事件相关信息的接口。继承自 <see cref="IGroupEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IGroupOperatingEventArgs : IGroupEventArgs, IOperatorEventArgs
    {
        
    }

    public class GroupOperatingEventArgs : OperatorEventArgs, IGroupOperatingEventArgs // 没法继承多个类, 强转接口吧
    {
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupOperatingEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupOperatingEventArgs(IGroupInfo group, IGroupMemberInfo @operator) : base(@operator)
        {
            Group = group;
        }
    }

    /// <summary>
    /// 提供群内成员被管理操作事件相关信息的接口。继承自 <see cref="IMemberEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IMemberOperatingEventArgs : IMemberEventArgs, IOperatorEventArgs
    {

    }

    public abstract class MemberOperatingEventArgs : OperatorEventArgs, IMemberOperatingEventArgs
    {
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberOperatingEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberOperatingEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(@operator)
        {
            Member = member;
        }
    }
}
