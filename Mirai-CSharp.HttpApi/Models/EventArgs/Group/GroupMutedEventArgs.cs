using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using Mirai.CSharp.Models.EventArgs;
using ISharedGroupMemberMutedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberMutedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供其它群成员被禁言事件相关信息的接口。继承自 <see cref="Mirai.CSharp.Models.EventArgs.IGroupMemberUnmutedEventArgs{TRawdata}"/> 和 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberMuteEvent")]
    public interface IGroupMemberMutedEventArgs : ISharedGroupMemberMutedEventArgs, IMiraiHttpMessage
    {
#if NETSTANDARD2_0
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        new TimeSpan Duration { get; }
#else
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        abstract TimeSpan IMutedEventArgs.Duration { get; }
#endif
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

#if NETSTANDARD2_0
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        [JsonPropertyName("durationSeconds")]
        TimeSpan IMutedEventArgs.Duration => Duration;
#endif
    }
}
