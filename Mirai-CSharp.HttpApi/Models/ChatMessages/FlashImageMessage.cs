using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedFlashImageMessage = Mirai.CSharp.Models.ChatMessages.IFlashImageMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedFlashImageMessage"/>
    [MappableMiraiChatMessageKey(FlashImageMessage.MsgType)]
    public interface IFlashImageMessage : ISharedFlashImageMessage, IChatMessage
    {

    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class FlashImageMessage : CommonImageMessage, IFlashImageMessage
    {
        public const string MsgType = "FlashImage";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <summary>
        /// 初始化 <see cref="FlashImageMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FlashImageMessage() : this(null, null, null)
        {

        }
        /// <summary>
        /// 初始化 <see cref="FlashImageMessage"/> 类的新实例
        /// </summary>
        /// <inheritdoc/>
        public FlashImageMessage(string? imageId, string? url, string? path) : base(imageId, url, path)
        {

        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:flashimage:{ImageId}]"; // 不同于源码实现。原同ImageMessage
    }
}
