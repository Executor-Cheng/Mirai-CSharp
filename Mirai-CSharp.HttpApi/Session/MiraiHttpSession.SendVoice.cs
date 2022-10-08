using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.Models;
using Mirai.CSharp.Services;
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
            if (voiceStream is MemoryStream ms)
            {
                byte[] buffer = new byte[ms.Length - ms.Position];
                ms.Read(buffer, 0, buffer.Length);
                return InternalUploadVoiceAsync(session, type, buffer, token);
            }
            async Task<ISharedVoiceMessage> Await(InternalSessionInfo session, UploadTarget type, Stream voiceStream, CancellationToken token = default)
            {
                using MemoryStream s = new MemoryStream();             
#if NETSTANDARD2_0
                await voiceStream.CopyToAsync(s);
#else
                await voiceStream.CopyToAsync(s, token);
#endif
                return await InternalUploadVoiceAsync(session, type, s.ToArray(), token);
            }
            return Await(session, type, voiceStream, token);
        }

        /// <summary>
        /// 内部使用
        /// </summary>
        private unsafe Task<ISharedVoiceMessage> InternalUploadVoiceAsync(InternalSessionInfo session, UploadTarget type, byte[] voice, CancellationToken token)
        {
            IVoiceConverter? converter = _services.GetService<IVoiceConverter>();
            if (converter != null)
            {
                if (!converter.TryConvert(voice, out byte[]? converted))
                {
                    throw new ArgumentException("输入的音频数据无法被已注册的 IVoiceConverter 转换", nameof(voice));
                }
                if (converted != null)
                {
                    voice = converted;
                }
            }
            MultipartFormDataContent payload = new MultipartFormDataContent(HttpClientExtensions.DefaultBoundary);
            payload.Add(new StringContent(session.SessionKey), "sessionKey");
            payload.Add(new StringContent(type.ToString().ToLower()), "type");
            payload.Add(new StringContent(type.ToString().ToLower()), "type");
            payload.Add(new ByteArrayContent(voice), "voice", $"{Guid.NewGuid():n}.silk");
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsync($"{_options.BaseUrl}/uploadVoice", payload, token)
                .AsApiRespAsync<ISharedVoiceMessage, VoiceMessage>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedVoiceMessage> UploadVoiceAsync(UploadTarget type, string voicePath, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            FileStream fs = new FileStream(voicePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return InternalUploadVoiceAsync(session, type, fs, token).DisposeWhenCompleted(fs);
        }
        /// <inheritdoc/>
        public override Task<ISharedVoiceMessage> UploadVoiceAsync(UploadTarget type, Stream voice, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadVoiceAsync(session, type, voice, token);
        }
    }
}
