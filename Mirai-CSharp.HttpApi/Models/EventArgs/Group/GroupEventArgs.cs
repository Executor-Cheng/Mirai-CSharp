using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群事件的信息接口
    /// </summary>
    public interface IGroupEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        IGroupInfo Group { get; }
    }

    public class GroupEventArgs : IGroupEventArgs,
                                  IBotJoinedGroupEventArgs,
                                  IBotPositiveLeaveGroupEventArgs,
                                  IBotKickedOutEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupEventArgs(GroupInfo group)
        {
            Group = group;
        }
    }

    /// <summary>
    /// 提供群成员信息的接口
    /// </summary>
    public interface IMemberEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        IGroupMemberInfo Member { get; }
    }

    public class MemberEventArgs : IMemberEventArgs,
                                   IGroupMemberJoinedEventArgs,
                                   IGroupMemberPositiveLeaveEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public MemberEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public MemberEventArgs(IGroupMemberInfo member)
        {
            Member = member;
        }
    }

    /// <summary>
    /// 提供群内操作者信息的接口
    /// </summary>
    public interface IOperatorEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        IGroupMemberInfo Operator { get; }
    }

    public class OperatorEventArgs : IOperatorEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        public virtual IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public OperatorEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public OperatorEventArgs(IGroupMemberInfo @operator)
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
    }

    /// <summary>
    /// 提供群内成员被管理操作事件相关信息的接口。继承自 <see cref="IMemberEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IMemberOperatingEventArgs : IMemberEventArgs, IOperatorEventArgs
    {

    }

    public class MemberOperatingEventArgs : OperatorEventArgs, IMemberOperatingEventArgs,
                                            IGroupMemberKickedEventArgs // 没法继承多个类, 强转接口吧
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public MemberOperatingEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public MemberOperatingEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(@operator)
        {
            Member = member;
        }
    }
}
