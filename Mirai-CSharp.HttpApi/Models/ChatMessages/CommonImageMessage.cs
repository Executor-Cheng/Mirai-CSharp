using System.Text.Json.Serialization;
using ISharedCommonImageMessage = Mirai.CSharp.Models.ChatMessages.ICommonImageMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedCommonImageMessage"/>
    public interface ICommonImageMessage : ISharedCommonImageMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedCommonImageMessage.ImageId"/>
        [JsonPropertyName("imageId")]
        new string? ImageId { get; set; }
        
        /// <inheritdoc cref="ISharedCommonImageMessage.Url"/>
        [JsonPropertyName("url")]
        new string? Url { get; set; }
        
        /// <inheritdoc cref="ISharedCommonImageMessage.Path"/>
        [JsonPropertyName("path")]
        new string? Path { get; set; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("imageId")]
        abstract string? ISharedCommonImageMessage.ImageId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("url")]
        abstract string? ISharedCommonImageMessage.Url { get; }

        /// <inheritdoc/>
        [JsonPropertyName("path")]
        abstract string? ISharedCommonImageMessage.Path { get; }
#endif
    }

    public abstract class CommonImageMessage : ChatMessage, ICommonImageMessage
    {
        /// <inheritdoc/>
        [JsonPropertyName("imageId")]
        public virtual string? ImageId { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("url")]
        public virtual string? Url { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("path")]
        public virtual string? Path { get; set; }
        /// <summary>
        /// 请子类重写Summary
        /// </summary>
        /// <param name="imageId">图片格式。不为空时将忽略 <paramref name="url"/> 参数
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </param>
        /// <param name="url">网络图片链接</param>
        /// <param name="path">本地图片路径。相对路径于 plugins/MiraiAPIHTTP/images</param>
        protected CommonImageMessage(string? imageId, string? url, string? path)
        {
            ImageId = imageId;
            Url = url;
            Path = path;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("imageId")]
        string? ISharedCommonImageMessage.ImageId => ImageId;

        /// <inheritdoc/>
        [JsonPropertyName("url")]
        string? ISharedCommonImageMessage.Url => Url;

        /// <inheritdoc/>
        [JsonPropertyName("path")]
        string? ISharedCommonImageMessage.Path => Path;
#endif
    }
}
