using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedAppMessage = Mirai.CSharp.Models.ChatMessages.IAppMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedAppMessage"/>
    [MappableMiraiChatMessageKey(AppMessage.MsgType)]
    public interface IAppMessage : ISharedAppMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedAppMessage.Content"/>
        [JsonPropertyName("content")]
        new string Content { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        abstract string ISharedAppMessage.Content { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class AppMessage : ChatMessage, IAppMessage
    {
        public const string MsgType = "App";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        public virtual string Content { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="AppMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public AppMessage() { }
        /// <summary>
        /// 初始化 <see cref="AppMessage"/> 类的新实例
        /// </summary>
        /// <param name="content">消息内容</param>
        public AppMessage(string content)
        {
            Content = content;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:app:{Content}]";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        string ISharedAppMessage.Content => Content;
#endif
    }
}
