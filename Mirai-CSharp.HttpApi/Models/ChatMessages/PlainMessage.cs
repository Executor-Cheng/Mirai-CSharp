using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedPlainMessage = Mirai.CSharp.Models.ChatMessages.IPlainMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedPlainMessage"/>
    [MappableMiraiChatMessageKey(PlainMessage.MsgType)]
    public interface IPlainMessage : ISharedPlainMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedPlainMessage.Message"/>
        [JsonPropertyName("text")]
        new string Message { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("text")]
        abstract string ISharedPlainMessage.Message { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class PlainMessage : ChatMessage, IPlainMessage
    {
        public const string MsgType = "Plain";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("text")]
        public virtual string Message { get; set; } = null!;
        /// <inheritdoc cref="PlainMessage(string)"/>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PlainMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="PlainMessage"/> 类的新实例
        /// </summary>
        /// <param name="message">文字消息内容</param>
        public PlainMessage(string message)
        {
            Message = message;
        }
        /// <inheritdoc/>
        public override string ToString()
            => Message;

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("text")]
        string ISharedPlainMessage.Message => Message;
#endif
    }
}
