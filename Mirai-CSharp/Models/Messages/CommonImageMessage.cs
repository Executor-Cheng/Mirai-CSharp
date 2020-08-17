using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 图片消息基类
    /// </summary>
    public abstract class CommonImageMessage : Messages
    {
        /// <summary>
        /// 图片的imageId, 群图片与好友图片格式不同。不为空时将忽略url属性
        /// </summary>
        /// <remarks>
        /// 格式如下:
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("imageId")]
        public string? ImageId { get; set; }
        /// <summary>
        /// 图片的URL, 发送时可作网络图片的链接；接收时为腾讯图片服务器的链接, 可用于图片下载
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        /// <summary>
        /// 图片的路径, 发送本地图片, 相对路径于 plugins/MiraiAPIHTTP/images
        /// </summary>
        [JsonPropertyName("path")]
        public string? Path { get; set; }
        /// <summary>
        /// 请子类重写Summary
        /// </summary>
        /// <param name="type">图片类型。供api或反序列化使用</param>
        /// <param name="imageId">图片格式。不为空时将忽略 <paramref name="url"/> 参数
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </param>
        /// <param name="url">网络图片链接</param>
        /// <param name="path">本地图片路径。相对路径于 plugins/MiraiAPIHTTP/images</param>
        protected CommonImageMessage(string type, string? imageId, string? url, string? path) : base(type)
        {
            ImageId = imageId;
            Url = url;
            Path = path;
        }
    }
}
