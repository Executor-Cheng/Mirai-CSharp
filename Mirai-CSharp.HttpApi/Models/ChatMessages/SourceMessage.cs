using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedSourceMessage = Mirai.CSharp.Models.ChatMessages.ISourceMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedSourceMessage"/>
    [MappableMiraiChatMessageKey(SourceMessage.MsgType)]
    public interface ISourceMessage : ISharedSourceMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedSourceMessage.Id"/>
        [JsonPropertyName("id")]
        new int Id { get; }
        /// <inheritdoc cref="ISharedSourceMessage.Time"/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        new DateTime Time { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        abstract int ISharedSourceMessage.Id { get; }
        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        abstract DateTime ISharedSourceMessage.Time { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class SourceMessage : ChatMessage, ISourceMessage
    {
        public const string MsgType = "Source";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public virtual int Id { get; set; }
        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public virtual DateTime Time { get; set; }
        /// <summary>
        /// 初始化 <see cref="SourceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="SourceMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">消息的识别号, 用于引用回复或撤回</param>
        /// <param name="time">消息时间</param>
        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage(int id, DateTime time)
        {
            Id = id;
            Time = time;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:source:{Id}]";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        int ISharedSourceMessage.Id => Id;
        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        DateTime ISharedSourceMessage.Time => Time;
#endif
    }
}
