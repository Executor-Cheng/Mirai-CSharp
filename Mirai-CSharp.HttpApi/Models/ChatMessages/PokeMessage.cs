using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.Models;
using ISharedPokeMessage = Mirai.CSharp.Models.ChatMessages.IPokeMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedPokeMessage"/>
    [MappableMiraiChatMessageKey(PokeMessage.MsgType)]
    public interface IPokeMessage : ISharedPokeMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedPokeMessage.Name"/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        new PokeType Name { get; }
#else
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        abstract PokeType ISharedPokeMessage.Name { get; }
#endif
    }

    /// <summary>
    /// 表示戳一戳消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class PokeMessage : ChatMessage, IPokeMessage
    {
        public const string MsgType = "Poke";

        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <summary>
        /// 戳一戳类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        public virtual PokeType Name { get; set; }
        /// <summary>
        /// 初始化 <see cref="PokeMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PokeMessage() { }
        /// <summary>
        /// 初始化 <see cref="PokeMessage"/> 类的新实例
        /// </summary>
        /// <param name="name">戳一戳的类型</param>
        public PokeMessage(PokeType name)
        {
            Name = name;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:poke:{(int)Name},-1]"; // id在PokeType∈[1,6]时固定为-1

#if NETSTANDARD2_0
         /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        PokeType ISharedPokeMessage.Name => Name;
#endif
    }
}
