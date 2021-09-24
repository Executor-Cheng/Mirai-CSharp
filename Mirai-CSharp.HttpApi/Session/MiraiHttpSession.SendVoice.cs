using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.Models;
using ISharedVoiceMessage = Mirai.CSharp.Models.ChatMessages.IVoiceMessage;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 内部使用
        /// </summary>
        private Task<ISharedVoiceMessage> InternalUploadVoiceAsync(InternalSessionInfo session, UploadTarget type, Stream voiceStream, CancellationToken token = default)
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
                FileName = $"{Guid.NewGuid():n}.silk"
            };
            HttpContent[] contents = new HttpContent[]
            {
                sessionKeyContent,
                typeContent,
                voiceContent
            };
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsync($"{_options.BaseUrl}/uploadVoice", contents, token)
                .AsApiRespAsync<ISharedVoiceMessage, VoiceMessage>(token)
                .DisposeWhenCompleted(cts);
        }
        /// <inheritdoc/>
        public override Task<ISharedVoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadVoiceAsync(session, type, new FileStream(voicePath, FileMode.Open, FileAccess.Read, FileShare.Read), token);
        }
        /// <inheritdoc/>
        public override Task<ISharedVoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadVoiceAsync(session, type, voice, token);
        }
    }
}
