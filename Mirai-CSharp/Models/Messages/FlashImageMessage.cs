using System;
using System.Diagnostics;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示闪照消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class FlashImageMessage : CommonImageMessage
    {
        public const string MsgType = "FlashImage";
        /// <summary>
        /// 初始化 <see cref="FlashImageMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FlashImageMessage() : this(null, null, null) { }
        /// <summary>
        /// 初始化 <see cref="FlashImageMessage"/> 类的新实例
        /// </summary>
        /// <param name="imageId">图片格式。不为空时将忽略 <paramref name="url"/> 参数
        /// <list type="number">
        /// <item><term>{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.mirai</term><description>群图片</description></item>
        /// <item><term>/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX</term><description>好友图片</description></item>
        /// </list>
        /// </param>
        /// <param name="url">网络图片链接</param>
        /// <param name="path">本地图片路径。相对路径于 plugins/MiraiAPIHTTP/images</param>
        public FlashImageMessage(string? imageId, string? url, string? path) : base(MsgType, imageId, url, path)
        {
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:flashimage:{ImageId}]"; // 不同于源码实现。原同ImageMessage
    }
}
