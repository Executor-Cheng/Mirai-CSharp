using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using Mirai.CSharp.Models.EventArgs;
using ISharedBotMutedEventArgs = Mirai.CSharp.Models.EventArgs.IBotMutedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被禁言事件相关信息的接口。继承自 <see cref="ISharedBotMutedEventArgs"/> 和 <see cref="IOperatorEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotMuteEvent")]
    public interface IBotMutedEventArgs : ISharedBotMutedEventArgs, IOperatorEventArgs
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
}
