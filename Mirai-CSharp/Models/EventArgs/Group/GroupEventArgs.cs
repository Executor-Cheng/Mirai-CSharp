using Mirai_CSharp.Utility.JsonConverters;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IGroupEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        IGroupInfo Group { get; }
    }

    public class GroupEventArgs : IGroupEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; }

        public GroupEventArgs() { }

        public GroupEventArgs(GroupInfo group)
        {
            Group = group;
        }
    }

    public interface IMemberEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        IGroupMemberInfo Member { get; }
    }

    public class MemberEventArgs : IMemberEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; }

        public MemberEventArgs() { }

        public MemberEventArgs(IGroupMemberInfo member)
        {
            Member = member;
        }
    }

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
        public virtual IGroupMemberInfo Operator { get; set; }

        public OperatorEventArgs() { }

        public OperatorEventArgs(IGroupMemberInfo @operator)
        {
            Operator = @operator;
        }
    }

    public interface IGroupOperatingEventArgs : IGroupEventArgs, IOperatorEventArgs
    {
        
    }

    public class GroupOperatingEventArgs : OperatorEventArgs, IGroupOperatingEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; }

        public GroupOperatingEventArgs() { }

        public GroupOperatingEventArgs(IGroupInfo group, IGroupMemberInfo @operator) : base(@operator)
        {
            Group = group;
        }
    } // 没法继承多个类, 强转接口吧

    public interface IMemberOperatingEventArgs : IMemberEventArgs, IOperatorEventArgs
    {

    }

    public class MemberOperatingEventArgs : OperatorEventArgs, IMemberOperatingEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("member")]
        public IGroupMemberInfo Member { get; set; }

        public MemberOperatingEventArgs() { }

        public MemberOperatingEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(@operator)
        {
            Member = member;
        }
    } // 没法继承多个类, 强转接口吧
}
