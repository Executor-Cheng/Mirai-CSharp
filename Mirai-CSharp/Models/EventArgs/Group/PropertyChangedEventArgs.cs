using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供修改前和修改后的 <typeparamref name="TProperty"/> 信息接口
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 修改前
        /// </summary>
        [JsonPropertyName("origin")]
        TProperty Origin { get; }
        /// <summary>
        /// 修改后
        /// </summary>
        [JsonPropertyName("current")]
        TProperty Current { get; }
    }

    public abstract class PropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 修改前
        /// </summary>
        [JsonPropertyName("origin")]
        public virtual TProperty Origin { get; set; } = default!;
        /// <summary>
        /// 修改后
        /// </summary>
        [JsonPropertyName("current")]
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
#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        new TProperty Origin { get; }

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        new TProperty Current { get; }
#else
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        abstract TProperty IPropertyChangedEventArgs<TProperty>.Origin { get; }

        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        abstract TProperty IPropertyChangedEventArgs<TProperty>.Current { get; }
#endif
    }

    public abstract class EnumPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty> where TProperty : Enum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        public override TProperty Origin { get => base.Origin; set => base.Origin = value; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        public override TProperty Current { get => base.Current; set => base.Current = value; }

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
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        public override TProperty Origin { get => base.Origin; set => base.Origin = value; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        public override TProperty Current { get => base.Current; set => base.Current = value; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotGroupEnumPropertyChangedEventArgs(IGroupInfo group, TProperty origin, TProperty current) : base(group, origin, current)
        {

        }
#if NETSTANDARD2_0
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Origin => base.Origin;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Current => base.Current;
#endif
    }

    /// <summary>
    /// 提供群属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IGroupOperatingEventArgs
    {

    }

    public /*abstract*/ class GroupPropertyChangedEventArgs<TProperty> : BotGroupPropertyChangedEventArgs<TProperty>, IGroupPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 操作者信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        public IGroupMemberInfo Operator { get; set; } = null!;

        /*protected*/
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs() { }

        /*protected*/
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupPropertyChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, TProperty origin, TProperty current) : base(group, origin, current)
        {
            Operator = @operator;
        }
    }

    //public class GroupNameChangedEventArgs : GroupPropertyChangedEventArgs<string>
    //{
    //    public GroupNameChangedEventArgs() { }

    //    public GroupNameChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupEntranceAnnouncementChangedEventArgs : GroupPropertyChangedEventArgs<string>
    //{
    //    public GroupEntranceAnnouncementChangedEventArgs() { }

    //    public GroupEntranceAnnouncementChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupMemberCardChangedEventArgs : GroupPropertyChangedEventArgs<string>
    //{
    //    public GroupMemberCardChangedEventArgs() { }

    //    public GroupMemberCardChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupSpecialTitleChangedEventArgs : GroupPropertyChangedEventArgs<string>
    //{
    //    public GroupSpecialTitleChangedEventArgs() { }

    //    public GroupSpecialTitleChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, string origin, string current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupMuteAllChangedEventArgs : GroupPropertyChangedEventArgs<bool>
    //{
    //    public GroupMuteAllChangedEventArgs() { }

    //    public GroupMuteAllChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupAnonymousChatChangedEventArgs : GroupPropertyChangedEventArgs<bool>
    //{
    //    public GroupAnonymousChatChangedEventArgs() { }

    //    public GroupAnonymousChatChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupConfessTalkChangedEventArgs : GroupPropertyChangedEventArgs<bool>
    //{
    //    public GroupConfessTalkChangedEventArgs() { }

    //    public GroupConfessTalkChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public class GroupMemberInviteChangedEventArgs : GroupPropertyChangedEventArgs<bool>
    //{
    //    public GroupMemberInviteChangedEventArgs() { }

    //    public GroupMemberInviteChangedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, bool origin, bool current) : base(group, @operator, origin, current)
    //    {

    //    }
    //}

    //public interface IGroupEnumPropertyChangedEventArgs<TProperty> : IBotGroupEnumPropertyChangedEventArgs<TProperty>, IGroupOperatingEventArgs where TProperty : Enum
    //{

    //}

    /// <summary>
    /// 提供群成员属性改变的信息接口。继承自 <see cref="IPropertyChangedEventArgs{TProperty}"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public interface IGroupMemberPropertyChangedEventArgs<TProperty> : IPropertyChangedEventArgs<TProperty>, IMemberEventArgs
    {

    }

    public /*abstract*/ class GroupMemberPropertyChangedEventArgs<TProperty> : PropertyChangedEventArgs<TProperty>, IGroupMemberPropertyChangedEventArgs<TProperty>
    {
        /// <summary>
        /// 被操作者信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        /*protected*/
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs() { }

        /*protected*/
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberPropertyChangedEventArgs(IGroupMemberInfo member, TProperty origin, TProperty current) : base(origin, current)
        {
            Member = member;
        }
    }

    //public class GroupMemberSpecialTitleChangedEventArgs : GroupMemberPropertyChangedEventArgs<string>
    //{
    //    public GroupMemberSpecialTitleChangedEventArgs()
    //    {

    //    }

    //    public GroupMemberSpecialTitleChangedEventArgs(IGroupMemberInfo member, string origin, string current) : base(member, origin, current)
    //    {

    //    }
    //}

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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        public override TProperty Origin { get => base.Origin; set => base.Origin = value; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        public override TProperty Current { get => base.Current; set => base.Current = value; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberEnumPropertyChangedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberEnumPropertyChangedEventArgs(IGroupMemberInfo member, TProperty origin, TProperty current) : base(member, origin, current)
        {

        }
#if NETSTANDARD2_0
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("origin")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Origin => base.Origin;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("current")]
        TProperty IEnumPropertyChangedEventArgs<TProperty>.Current => base.Current;
#endif
    } // 不能继承多个类, 只能每次都啰嗦两次override
}
