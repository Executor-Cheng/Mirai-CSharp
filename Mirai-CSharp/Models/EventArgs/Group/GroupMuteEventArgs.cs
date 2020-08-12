using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

#nullable enable
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供禁言信息的接口
    /// </summary>
    public interface IMutedEventArgs
    {
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        TimeSpan Duration { get; }
    }

    /// <summary>
    /// 提供Bot被解除禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IBotUnmutedEventArgs : IOperatorEventArgs
    {

    }

    public class BotUnmutedEventArgs : OperatorEventArgs, IBotUnmutedEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public BotUnmutedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotUnmutedEventArgs(GroupMemberInfo @operator) : base(@operator)
        {

        }
    }

    /// <summary>
    /// 提供Bot被禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/> 和 <see cref="IMutedEventArgs"/>
    /// </summary>
    public interface IBotMutedEventArgs : IOperatorEventArgs, IMutedEventArgs
    {

    }

    public class BotMutedEventArgs : OperatorEventArgs, IBotMutedEventArgs
    {
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        public TimeSpan Duration { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotMutedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotMutedEventArgs(TimeSpan duration, GroupMemberInfo @operator) : base(@operator)
        {
            Duration = duration;
        }
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
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
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
