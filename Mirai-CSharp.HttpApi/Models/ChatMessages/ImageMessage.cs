using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedImageMessage = Mirai.CSharp.Models.ChatMessages.IImageMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedImageMessage"/>
    [MappableMiraiChatMessageKey(ImageMessage.MsgType)]
    public interface IImageMessage : ISharedImageMessage, IChatMessage
    {

    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class ImageMessage : CommonImageMessage, IImageMessage
    {
        public const string MsgType = "Image";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <summary>
        /// 初始化 <see cref="ImageMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public ImageMessage() : this(null, null, null)
        {

        }
        /// <summary>
        /// 初始化 <see cref="ImageMessage"/> 类的新实例
        /// </summary>
        /// <inheritdoc/>
        public ImageMessage(string? imageId, string? url, string? path) : base(imageId, url, path)
        {

        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:image:{ImageId}]";
    }
}
