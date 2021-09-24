namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示图片消息的基接口
    /// </summary>
    public interface ICommonImageMessage : IChatMessage
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
        string? ImageId { get; }
        /// <summary>
        /// 图片的URL, 发送时可作网络图片的链接；接收时为腾讯图片服务器的链接, 可用于图片下载
        /// </summary>
        string? Url { get; }
        /// <summary>
        /// 图片的路径, 发送本地图片, 相对路径于 plugins/MiraiAPIHTTP/images
        /// </summary>
        string? Path { get; }
    }
}
