using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedAtMessage = Mirai.CSharp.Models.ChatMessages.IAtMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedAtMessage"/>
    [MappableMiraiChatMessageKey(AtMessage.MsgType)]
    public interface IAtMessage : ISharedAtMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedAtMessage.Target"/>
        [JsonPropertyName("target")]
        new long Target { get; }
        /// <inheritdoc cref="ISharedAtMessage.Display"/>
        [JsonPropertyName("display")]
        new string? Display { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("target")]
        abstract long ISharedAtMessage.Target { get; }
        /// <inheritdoc/>
        [JsonPropertyName("display")]
        abstract string? ISharedAtMessage.Display { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class AtMessage : ChatMessage, IAtMessage
    {
        public const string MsgType = "At";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("target")]
        public virtual long Target { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("display")]
        public virtual string? Display { get; set; }
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage() { }
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        /// <param name="target">要@的群员QQ号</param>
        public AtMessage(long target)
        {
            Target = target;
        }
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        /// <param name="target">要@的群员QQ号</param>
        /// <param name="display">@时显示的文字</param>
        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage(long target, string display) : this(target)
        {
            Display = display;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:at:{Target}]";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("target")]
        long ISharedAtMessage.Target => Target;
        /// <inheritdoc/>
        [JsonPropertyName("display")]
        string? ISharedAtMessage.Display => Display;
#endif
    }
}
