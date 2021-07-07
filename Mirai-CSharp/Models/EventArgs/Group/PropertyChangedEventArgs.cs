using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供修改前和修改后的 <typeparamref name="TProperty"/> 信息接口
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IPropertyChangedEventArgs<TProperty> : IEventArgsBase
    {
        /// <summary>
        /// 修改前
        /// </summary>
        TProperty Origin { get; }
        /// <summary>
        /// 修改后
        /// </summary>
        TProperty Current { get; }
    }

    public abstract class PropertyChangedEventArgs<TProperty> : EventArgsBase, IPropertyChangedEventArgs<TProperty>
    {
        /// <inheritdoc/>
        public virtual TProperty Origin { get; set; } = default!;
        /// <inheritdoc/>
        public virtual TProperty Current { get; set; } = default!;

        protected PropertyChangedEventArgs() { }

        protected PropertyChangedEventArgs(TProperty origin, TProperty current)
        {
            Origin = origin;
            Current = current;
        }
    }

    /// <summary>
    /// 提供修改前和修改后的 <typeparamref name="TProperty"/> 信息接口
    /// </summary>
    /// <remarks>
    /// 本接口是对于 <see langword="enum"/> 的特定实现
    /// </remarks>
    /// <typeparam name="TProperty">属性类型, 必须为枚举类型</typeparam>
    public interface IEnumPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
         
    }

    public abstract class EnumPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        protected EnumPropertyChangedEventArgs()
        {

        }

        protected EnumPropertyChangedEventArgs(TProperty origin, TProperty current) : base(origin, current)
        {

        }
    }

    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IBotGroupPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IGroupEventArgs
    {
        
    }

    public abstract class BotGroupPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IBotGroupPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 机器人所在群信息
        /// </summary>
        public IGroupInfo Group { get; set; } = null!;

        protected BotGroupPropertyChangedEventArgs() { }

        protected BotGroupPropertyChangedEventArgs(IGroupInfo group, TProperty origin, TProperty current) : base(origin, current)
        {
            Group = group;
        }
    }

    /// <summary>
    /// 提供Bot在群中属性改变的信息接口。继承自 <see cref="IEnumPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    /// <remarks>
    /// 本接口是对于 <see langword="enum"/> 的特定实现
    /// </remarks>
    /// <typeparam name="TProperty">属性类型, 必须为枚举类型</typeparam>
    public interface IBotGroupEnumPropertyChangedEventArgs<TProperty> : IEnumPropertyChangedEventArgs<TProperty>, IGroupEventArgs where TProperty : Enum
    {

    }

    public class BotGroupEnumPropertyChangedEventArgs<TProperty> : BotGroupPropertyChangedEventArgs<TProperty>, IBotGroupEnumPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs(IGroupInfo group, TProperty origin, TProperty current) : base(group, origin, current)
        {

        }
    }

    /// <summary>
    /// 提供群属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IGroupOperatingEventArgs
    {

    }

    public class GroupPropertyChangedEventArgs<TProperty> : BotGroupPropertyChangedEventArgs<TProperty>, IGroupPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 操作者信息
        /// </summary>
        public IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, TProperty origin, TProperty current) : base(group, origin, current)
        {
            Operator = @operator;
        }
    }

    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupMemberPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IMemberEventArgs
    {

    }

    public class GroupMemberPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IGroupMemberPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 被操作者信息
        /// </summary>
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs(IGroupMemberInfo member, TProperty origin, TProperty current) : base(origin, current)
        {
            Member = member;
        }
    }

    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="IEnumPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    /// <remarks>
    /// 本接口是对于 <see langword="enum"/> 的特定实现
    /// </remarks>
    /// <typeparam name="TProperty">属性类型, 必须为枚举类型</typeparam>
    public interface IGroupMemberEnumPropertyChangedEventArgs<TProperty> : IEnumPropertyChangedEventArgs<TProperty>, IMemberEventArgs where TProperty : Enum
    {
        
    }

    public class GroupMemberEnumPropertyChangedEventArgs<TProperty> : GroupMemberPropertyChangedEventArgs<TProperty>, IGroupMemberEnumPropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberEnumPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberEnumPropertyChangedEventArgs(IGroupMemberInfo member, TProperty origin, TProperty current) : base(member, origin, current)
        {

        }
    }
}
