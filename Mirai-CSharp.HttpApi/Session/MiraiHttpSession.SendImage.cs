using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Utility;
using Mirai.CSharp.Models;
using SkiaSharp;
using ISharedImageMessage = Mirai.CSharp.Models.ChatMessages.IImageMessage;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 内部使用
        /// </summary>
        private Task<string[]> CommonSendImageAsync(long? qqNumber, long? groupNumber, string[] urls, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            if (urls == null || urls.Length == 0)
            {
                throw new ArgumentException("urls必须为非空且至少有1条url。");
            }
            var payload = new
            {
                sessionKey = session.SessionKey,
                qq = qqNumber,
                group = groupNumber,
                urls
            };
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/sendImageMessage", payload, JsonSerializeOptionsFactory.IgnoreNulls, token)
                .AsApiRespV2Async<string[]>(token)
                .DisposeWhenCompleted(cts);
        }
        /// <inheritdoc/>
        public override Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls, CancellationToken token = default)
        {
            return CommonSendImageAsync(qqNumber, null, urls, token);
        }
        /// <inheritdoc/>
        public override Task<string[]> SendImageToTempAsync(long qqNumber, long groupNumber, string[] urls, CancellationToken token = default)
        {
            return CommonSendImageAsync(qqNumber, groupNumber, urls, token);
        }
        /// <inheritdoc/>
        public override Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls, CancellationToken token = default)
        {
            return CommonSendImageAsync(null, groupNumber, urls, token);
        }
        /// <summary>
        /// 内部使用
        /// </summary>
        private async Task<ISharedImageMessage> InternalUploadPictureAsync(InternalSessionInfo session, UploadTarget type, Stream imgStream, bool disposeStream, CancellationToken token = default)
        {
            // 使用 disposeStream 目的是不使上层 caller 创建多余的状态机
            if (session.ApiVersion <= new Version(1, 7, 0))
            {
                Guid guid = Guid.NewGuid();
                MemoryStream ms = new MemoryStream(8192); // 无论如何都做一份copy
                await imgStream.CopyToAsync(ms, 81920, token).ConfigureAwait(false);
                ImageHttpListener.RegisterImage(guid, ms);
                return new ImageMessage(null, $"http://127.0.0.1:{ImageHttpListener.Port}/fetch?guid={guid:n}", null);
            }
            Stream? internalStream = null;
            bool internalCreated = false;
            long pervious = 0;
            if (!imgStream.CanSeek || imgStream.CanTimeout) // 对于 CanTimeOut 的 imgStream, 或者无法Seek的, 一律假定其读取行为是阻塞的
                                                            // 为其创建一个内部缓存先行异步读取
            {
                internalStream = new MemoryStream(8192);
                internalCreated = true;
                await imgStream.CopyToAsync(internalStream, 81920, token);
            }
            else // 否则不创建副本, 避免多余的堆分配
            {
                internalStream = imgStream;
                pervious = imgStream.Position;
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
            string format;
            using SKManagedStream skstream = new SKManagedStream(internalStream, false);
            using (SKCodec codec = SKCodec.Create(skstream)) // 已经把数据读到非托管内存里边了, 就不用管input的死活了
            {
                var skformat = codec.EncodedFormat;
                format = skformat.ToString().ToLower();
                switch (skformat)
                {
                    case SKEncodedImageFormat.Gif:
                    case SKEncodedImageFormat.Jpeg:
                    case SKEncodedImageFormat.Png:
                        break;
                    default:
                        {
                            skstream.Seek(0);
                            using (SKBitmap bitmap = SKBitmap.Decode(skstream))
                            {
                                if (!internalCreated)
                                {
                                    internalStream = new MemoryStream(8192);
                                    internalCreated = true;
                                }
                                else
                                {
                                    internalStream.Seek(0, SeekOrigin.Begin);
                                }
                                bitmap.Encode(internalStream, SKEncodedImageFormat.Png, 100);
                            }
                            format = "png";
                            break;
                        }
                }
            }
            if (internalCreated)
            {
                internalStream.Seek(0, SeekOrigin.Begin);
            }
            else // internalStream == imgStream
            {
                internalStream.Seek(pervious - internalStream.Position, SeekOrigin.Current);
            }
            HttpContent imageContent = new StreamContent(internalStream);
            imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "img",
                FileName = $"{Guid.NewGuid():n}.{format}"
            };
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/" + format);
            HttpContent[] contents = new HttpContent[]
            {
                    sessionKeyContent,
                    typeContent,
                    imageContent
            };
            try
            {
                CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
                return await _client.PostAsync($"{_options.BaseUrl}/uploadImage", contents, token)
                    .AsApiRespAsync<ISharedImageMessage, ImageMessage>(token)
                    .DisposeWhenCompleted(cts)
                    .ContinueWith(t => t.IsFaulted && t.Exception!.InnerException is JsonException ? throw new NotSupportedException("当前版本的mirai-api-http无法发送图片。") : t, TaskContinuationOptions.ExecuteSynchronously).Unwrap().ConfigureAwait(false);
                    //  ^-- 处理 JsonException 到 NotSupportedException, https://github.com/mamoe/mirai-api-http/issues/85
                    // internalStream 是 MemoryStream, 内部为全托管字段不需要 Dispose
            }
            finally
            {
                if (disposeStream)
                {
                    imgStream.Dispose();
                }
            }
        }
        /// <remarks>
        /// 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// <para/>
        /// <paramref name="imagePath"/> 会被读取至末尾
        /// </remarks>
        /// <inheritdoc/>
        public override Task<ISharedImageMessage> UploadPictureAsync(UploadTarget type, string imagePath, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read), true, token);
        }
        /// <remarks>
        /// 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// <para/>
        /// <paramref name="image"/> 会被读取至末尾
        /// </remarks>
        /// <inheritdoc/>
        public override Task<ISharedImageMessage> UploadPictureAsync(UploadTarget type, Stream image, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, image, false, token);
        }
    }
}
