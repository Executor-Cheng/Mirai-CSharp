using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

#nullable enable
namespace Mirai_CSharp.Models
{
    public interface IMutedEventArgs
    {
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        TimeSpan Duration { get; }
    }

    public interface IBotUnmutedEventArgs : IOperatorEventArgs
    {

    }

    public class BotUnmutedEventArgs : OperatorEventArgs, IBotUnmutedEventArgs
    {
        public BotUnmutedEventArgs()
        {

        }

        public BotUnmutedEventArgs(GroupMemberInfo @operator) : base(@operator)
        {

        }
    }

    public interface IBotMutedEventArgs : IOperatorEventArgs, IMutedEventArgs
    {

    }

    public class BotMutedEventArgs : OperatorEventArgs, IBotMutedEventArgs
    {
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        public TimeSpan Duration { get; set; }

        public BotMutedEventArgs() { }

        public BotMutedEventArgs(TimeSpan duration, GroupMemberInfo @operator) : base(@operator)
        {
            Duration = duration;
        }
    }

    public interface IMemberUnmutedEventArgs : IMemberOperatingEventArgs
    {

    }

    public class MemberUnmutedEventArgs : MemberOperatingEventArgs, IMemberUnmutedEventArgs
    {
        public MemberUnmutedEventArgs()
        {

        }

        public MemberUnmutedEventArgs(IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {

        }
    }

    public interface IMemberMutedEventArgs : IMemberOperatingEventArgs, IMutedEventArgs 
    {
    
    }

    public class MemberMutedEventArgs : MemberOperatingEventArgs, IMemberMutedEventArgs
    {
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        public TimeSpan Duration { get; set; }

        public MemberMutedEventArgs()
        {

        }

        public MemberMutedEventArgs(TimeSpan duration, IGroupMemberInfo member, IGroupMemberInfo @operator) : base(member, @operator)
        {
            Duration = duration;
        }
    }
}
