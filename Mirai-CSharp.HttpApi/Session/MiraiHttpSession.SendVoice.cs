using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        private static ReadOnlySpan<byte> _silkHeader => new byte[10] { 2, 35, 33, 83, 73, 76, 75, 95, 86, 51 };

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
        private unsafe Task<ISharedVoiceMessage> InternalUploadVoiceAsync(InternalSessionInfo session, UploadTarget type, byte[] silkData, CancellationToken token)
        {
            if (silkData.Length <= 10)
            {
                throw new ArgumentException("输入的音频数据无效", nameof(silkData));
            }
            Span<byte> silkSpan = silkData.AsSpan();
            if (silkSpan.Slice(0, 9).SequenceEqual(_silkHeader.Slice(1)))
            {
                byte[] copied = new byte[silkData.Length + 1];
                copied[0] = 2;
#if NET5_0_OR_GREATER
                Unsafe.CopyBlock(ref Unsafe.AddByteOffset(ref MemoryMarshal.GetArrayDataReference(copied), new IntPtr(1)), ref MemoryMarshal.GetReference(silkSpan), (uint)silkData.Length);
#else
                Unsafe.CopyBlock(ref copied[1], ref MemoryMarshal.GetReference(silkSpan), (uint)silkData.Length);
#endif
                silkData = copied;
            }
            else if (!silkSpan.SequenceEqual(_silkHeader))
            {
                if (_coder == null)
                {
                    throw new InvalidOperationException("由于未注册 ISilkLameCoder 服务, 无法发送非silk格式的音频");
                }
                if (_coder.TryEncodeMp3ToSilk(silkSpan, out silkData!) != 0)
                {
                    throw new InvalidOperationException("输入的音频格式不为mp3, 因此无法将其转为silk格式进行发送");
                }
            }
            MultipartFormDataContent payload = new MultipartFormDataContent(HttpClientExtensions.DefaultBoundary);
            payload.Add(new StringContent(session.SessionKey), "sessionKey");
            payload.Add(new StringContent(type.ToString().ToLower()), "type");
            payload.Add(new StringContent(type.ToString().ToLower()), "type");
            payload.Add(new ByteArrayContent(silkData), "voice", $"{Guid.NewGuid():n}.silk");
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
