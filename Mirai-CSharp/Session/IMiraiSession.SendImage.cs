using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Exceptions;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步发送给定Url数组中的图片到给定好友
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>一组ImageId</returns>
        Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls, CancellationToken token = default);

        /// <summary>
        /// 异步发送给定Url数组中的图片到群
        /// </summary>
        /// <exception cref="BotMutedException"/>
        /// <inheritdoc cref="SendImageToTempAsync(long, long, string[], CancellationToken)"/>
        Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls, CancellationToken token = default);

        /// <summary>
        /// 异步发送给定Url数组中的图片到临时会话
        /// </summary>
        /// <param name="groupNumber">目标QQ号所在的群号</param>
        /// <inheritdoc cref="SendImageToFriendAsync(long, string[], CancellationToken)"/>
        Task<string[]> SendImageToTempAsync(long qqNumber, long groupNumber, string[] urls, CancellationToken token = default);

        /// <param name="image">图片流</param>
        /// <inheritdoc cref="UploadPictureAsync(UploadTarget, string, CancellationToken)"/>
        Task<IImageMessage> UploadPictureAsync(UploadTarget type, Stream image, CancellationToken token = default);

        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="imagePath">图片路径</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>一个 <see cref="IImageMessage"/> 实例, 可用于以后的消息发送</returns>
        Task<IImageMessage> UploadPictureAsync(UploadTarget type, string imagePath, CancellationToken token = default);
    }
}
