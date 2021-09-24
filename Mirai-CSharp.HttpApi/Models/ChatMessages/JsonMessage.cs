using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedJsonMessage = Mirai.CSharp.Models.ChatMessages.IJsonMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedJsonMessage"/>
    [MappableMiraiChatMessageKey(JsonMessage.MsgType)]
    public interface IJsonMessage : ISharedJsonMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedJsonMessage.Json"/>
        [JsonPropertyName("json")]
        new string Json { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("json")]
        abstract string ISharedJsonMessage.Json { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class JsonMessage : ChatMessage, IJsonMessage
    {
        public const string MsgType = "Json";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("json")]
        public virtual string Json { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="JsonMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public JsonMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="JsonMessage"/> 类的新实例
        /// </summary>
        /// <param name="json">要发送的原始Json字符串</param>
        public JsonMessage(string json)
        {
            Json = json;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:service:1,{Json}]"; // Json的ServiceId=1, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("json")]
        string ISharedJsonMessage.Json => Json;
#endif
    }
}
