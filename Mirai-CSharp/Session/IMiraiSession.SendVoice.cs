using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;
using Mirai.CSharp.Models.ChatMessages;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <param name="voice">语音流。此流将被读取至末尾</param>
        /// <inheritdoc cref="UploadVoiceAsync(UploadTarget, string, CancellationToken)"/>
        Task<IVoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice, CancellationToken token = default);

        /// <summary>
        /// 异步上传语音
        /// </summary>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        /// <param name="type">目标类型</param>
        /// <param name="voicePath">语音路径</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>一个 <see cref="IVoiceMessage"/> 实例, 可用于以后的消息发送</returns>
        Task<IVoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath, CancellationToken token = default);
    }
}
