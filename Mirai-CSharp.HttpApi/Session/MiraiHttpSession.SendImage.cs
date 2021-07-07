using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using SkiaSharp;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有) // 已经 inheritdocs, 警告无效
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="groupNumber">目标QQ号所在的群号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        /// <returns>一组ImageId</returns>
        private Task<string[]> CommonSendImageAsync(long? qqNumber, long? groupNumber, string[] urls)
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
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/sendImageMessage", payload, JsonSerializeOptionsFactory.IgnoreNulls, session.Token)
                .AsApiRespAsync<string[]>();
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到给定好友
        /// </summary>
        /// <inheritdoc cref="CommonSendImageAsync"/>
        public Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls)
        {
            return CommonSendImageAsync(qqNumber, null, urls);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到临时会话
        /// </summary>
        /// <inheritdoc cref="CommonSendImageAsync"/>
        public Task<string[]> SendImageToTempAsync(long qqNumber, long groupNumber, string[] urls)
        {
            return CommonSendImageAsync(qqNumber, groupNumber, urls);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到群
        /// </summary>
        /// <exception cref="BotMutedException"/>
        /// <inheritdoc cref="CommonSendImageAsync"/>
        public Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls)
        {
            return CommonSendImageAsync(null, groupNumber, urls);
        }
        /// <summary>
        /// 内部使用
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="imgStream">图片流</param>
        /// <remarks>
        /// 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// <para/>
        /// <paramref name="imgStream"/> 会被读取至末尾
        /// </remarks>
        /// <returns>一个 <see cref="ImageMessage"/> 实例, 可用于以后的消息发送</returns>
        private static async Task<ImageMessage> InternalUploadPictureAsync(InternalSessionInfo session, UploadTarget type, Stream imgStream, bool disposeStream)
        {
            // 使用 disposeStream 目的是不使上层 caller 创建多余的状态机
            if (session.ApiVersion <= new Version(1, 7, 0))
            {
                Guid guid = Guid.NewGuid();
                MemoryStream ms = new MemoryStream(8192); // 无论如何都做一份copy
                await imgStream.CopyToAsync(ms);
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
                await imgStream.CopyToAsync(internalStream);
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
                return await session.Client.PostAsync($"{session.Options.BaseUrl}/uploadImage", contents, session.Token)
                .AsApiRespAsync<ImageMessage>(session.Token)
                .ContinueWith(t => t.IsFaulted && t.Exception!.InnerException is JsonException ? throw new NotSupportedException("当前版本的mirai-api-http无法发送图片。") : t, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
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
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <param name="imagePath">图片路径</param>
        /// <inheritdoc cref="InternalUploadPictureAsync"/>
        public Task<ImageMessage> UploadPictureAsync(UploadTarget type, string imagePath)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read), true);
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <param name="image">图片流</param>
        /// <remarks>
        /// <paramref name="image"/> 会被读取至末尾
        /// </remarks>
        /// <inheritdoc cref="InternalUploadPictureAsync"/>
        public Task<ImageMessage> UploadPictureAsync(UploadTarget type, Stream image)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, image, false);
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <param name="imagePath">图片路径</param>
        /// <inheritdoc cref="InternalUploadPictureAsync"/>
        [Obsolete("请调用 UploadPictureAsync(UploadTarget, string)")]
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, string imagePath)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, (UploadTarget)(int)type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read), true);
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <param name="image">图片流</param>
        /// <remarks>
        /// <paramref name="image"/> 会被读取至末尾
        /// </remarks>
        /// <inheritdoc cref="InternalUploadPictureAsync"/>
        [Obsolete("请调用 UploadPictureAsync(UploadTarget, Stream)")]
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, Stream image)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, (UploadTarget)(int)type, image, false);
        }
    }
}
