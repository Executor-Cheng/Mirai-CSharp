using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupEventArgs = Mirai.CSharp.Models.EventArgs.IGroupEventArgs;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedGroupOperatingEventArgs = Mirai.CSharp.Models.EventArgs.IGroupOperatingEventArgs;
using ISharedJsonGroupEventArgs = Mirai.CSharp.Models.EventArgs.IGroupEventArgs<System.Text.Json.JsonElement>;
using ISharedJsonMemberEventArgs = Mirai.CSharp.Models.EventArgs.IMemberEventArgs<System.Text.Json.JsonElement>;
using ISharedJsonOperatorEventArgs = Mirai.CSharp.Models.EventArgs.IOperatorEventArgs<System.Text.Json.JsonElement>;
using ISharedMemberEventArgs = Mirai.CSharp.Models.EventArgs.IMemberEventArgs;
using ISharedMemberOperatingEventArgs = Mirai.CSharp.Models.EventArgs.IMemberOperatingEventArgs;
using ISharedOperatorEventArgs = Mirai.CSharp.Models.EventArgs.IOperatorEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <inheritdoc cref="ISharedGroupEventArgs"/>
    public interface IGroupEventArgs : ISharedJsonGroupEventArgs, IMiraiHttpMessage
    {
        /// <inheritdoc cref="ISharedGroupEventArgs.Group"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        new IGroupInfo Group { get; }

#if !NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupEventArgs.Group => Group;
#endif
    }

    public abstract class GroupEventArgs : MiraiHttpMessage, IGroupEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected GroupEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected GroupEventArgs(GroupInfo group)
        {
            Group = group;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupEventArgs.Group => Group;
#endif
    }

    /// <inheritdoc cref="ISharedMemberEventArgs"/>
    public interface IMemberEventArgs : ISharedJsonMemberEventArgs, IMiraiHttpMessage
    {
        /// <inheritdoc cref="ISharedMemberEventArgs.Member"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        new IGroupMemberInfo Member { get; }

#if !NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("member")]
        ISharedGroupMemberInfo ISharedMemberEventArgs.Member => Member;
#endif
    }

    public abstract class MemberEventArgs : MiraiHttpMessage, IMemberEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberEventArgs(IGroupMemberInfo member)
        {
            Member = member;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("member")]
        ISharedGroupMemberInfo ISharedMemberEventArgs.Member => Member;
#endif
    }

    /// <inheritdoc cref="ISharedOperatorEventArgs"/>
    public interface IOperatorEventArgs : ISharedJsonOperatorEventArgs, IMiraiHttpMessage
    {
        /// <inheritdoc cref="ISharedOperatorEventArgs.Operator"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        new IGroupMemberInfo Operator { get; }

#if !NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        ISharedGroupMemberInfo ISharedOperatorEventArgs.Operator => Operator;
#endif
    }

    public abstract class OperatorEventArgs : MiraiHttpMessage, IOperatorEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        public virtual IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected OperatorEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected OperatorEventArgs(IGroupMemberInfo @operator)
        {
            Operator = @operator;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        ISharedGroupMemberInfo ISharedOperatorEventArgs.Operator => Operator;
#endif
    }

    /// <summary>
    /// 提供群内管理事件相关信息的接口。继承自 <see cref="ISharedGroupOperatingEventArgs"/>, <see cref="IGroupEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IGroupOperatingEventArgs : ISharedGroupOperatingEventArgs, IGroupEventArgs, IOperatorEventArgs
    {

    }

    public class GroupOperatingEventArgs : OperatorEventArgs, IGroupOperatingEventArgs // 没法继承多个类, 强转接口吧
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupOperatingEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupOperatingEventArgs(IGroupInfo group, IGroupMemberInfo @operator) : base(@operator)
        {
            Group = group;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupEventArgs.Group => Group;
#endif
    }

    /// <summary>
    /// 提供群内成员被管理操作事件相关信息的接口。继承自 <see cref="ISharedMemberOperatingEventArgs"/>, <see cref="IMemberEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IMemberOperatingEventArgs : ISharedMemberOperatingEventArgs, IMemberEventArgs, IOperatorEventArgs
    {

    }

    public abstract class MemberOperatingEventArgs : OperatorEventArgs, IMemberOperatingEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberOperatingEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected MemberOperatingEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(@operator)
        {
            Member = member;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("member")]
        ISharedGroupMemberInfo ISharedMemberEventArgs.Member => Member;
#endif
    }
}
