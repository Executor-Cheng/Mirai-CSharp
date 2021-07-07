using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有) // 已经 inheritdocs, 警告无效
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        /// <param name="type">目标类型</param>
        /// <param name="voiceStream">语音流</param>
        /// <returns>一个 <see cref="VoiceMessage"/> 实例, 可用于以后的消息发送</returns>
        private static Task<VoiceMessage> InternalUploadVoiceAsync(InternalSessionInfo session, UploadTarget type, Stream voiceStream)
        {
            if (session.ApiVersion < new Version(1, 8, 0))
            {
                throw new NotSupportedException($"当前版本的mirai-api-http不支持上传语音。({session.ApiVersion}, 必须>=1.8.0)");
            }
            HttpContent sessionKeyContent = new StringContent(session.SessionKey);
            sessionKeyContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "sessionKey"
            };
            HttpContent typeContent = new StringContent(type.ToString().ToLower());
            typeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "type"
            };
            HttpContent voiceContent = new StreamContent(voiceStream);
            voiceContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "voice",
                FileName = $"{Guid.NewGuid():n}.amr"
            };
            HttpContent[] contents = new HttpContent[]
            {
                sessionKeyContent,
                typeContent,
                voiceContent
            };
            return session.Client.PostAsync($"{session.Options.BaseUrl}/uploadVoice", contents, session.Token)
                .AsApiRespAsync<VoiceMessage>(session.Token);
        }
        /// <summary>
        /// 异步上传语音
        /// </summary>
        /// <exception cref="FileNotFoundException"/>
        /// <param name="voicePath">语音路径</param>
        /// <inheritdoc cref="InternalUploadVoiceAsync"/>
        public Task<VoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadVoiceAsync(session, type, new FileStream(voicePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
        /// <summary>
        /// 异步上传语音
        /// </summary>
        /// <param name="voice">语音流</param>
        /// <inheritdoc cref="InternalUploadVoiceAsync"/>
        public Task<VoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadVoiceAsync(session, type, voice);
        }
    }
}
