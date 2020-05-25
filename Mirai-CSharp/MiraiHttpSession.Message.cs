using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Helpers;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private async Task<int> CommonSendMessageAsync(string action, long? qqNumber, long? fromGroup, IMessageBase[] chain, int? quoteMsgId)
        {
            CheckConnected();
            if (chain == null || chain.Length == 0)
            {
                throw new ArgumentException("消息链必须为非空且至少有1条消息。");
            }
            JsonSerializerOptions opts = JsonSerializeOptionsFactory.IgnoreNulls;
            opts.Converters.Add(new IMessageBaseArrayConverter());
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                qq = qqNumber,
                group = fromGroup,
                quote = quoteMsgId,
                messageChain = chain
            }, opts);
            using JsonDocument j = await HttpHelper.HttpPostAsync($"{SessionInfo.Options.BaseUrl}/{action}", payload);
            JsonElement root = j.RootElement;
            int code = root.GetProperty("code").GetInt32();
            if (code == 0)
            {
                return root.GetProperty("messageId").GetInt32();
            }
            return ThrowCommonException<int>(code, in root);
        }
        /// <summary>
        /// 异步发送好友消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        public Task<int> SendFriendMessageAsync(long qqNumber, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendFriendMessage", qqNumber, null, chain, quoteMsgId);
        }
        /// <summary>
        /// 异步发送临时消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="fromGroup">目标所在的群号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        public Task<int> SendTempMessageAsync(long qqNumber, long fromGroup, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendTempMessage", qqNumber, fromGroup, chain, quoteMsgId);
        }
        /// <summary>
        /// 异步发送群消息
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="BotMutedException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="MessageTooLongException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">目标群号</param>
        /// <param name="chain">消息链数组。不可为 <see langword="null"/> 或空数组</param>
        /// <param name="quoteMsgId">引用一条消息的messageId进行回复。为 <see langword="null"/> 时不进行引用。</param>
        public Task<int> SendGroupMessageAsync(long groupNumber, IMessageBase[] chain, int? quoteMsgId = null)
        {
            return CommonSendMessageAsync("sendGroupMessage", null, groupNumber, chain, quoteMsgId);
        }

        private Task<string[]> CommonSendImageAsync(long? qqNumber, long? groupNumber, string[] urls)
        {
            CheckConnected();
            if (urls == null || urls.Length == 0)
            {
                throw new ArgumentException("urls必须为非空且至少有1条url。");
            }
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                qq = qqNumber,
                group = groupNumber,
                urls
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            return InternalHttpPostAsync<string[], string[]>($"{SessionInfo.Options.BaseUrl}/sendImageMessage", payload, SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到给定好友
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls)
        {
            return CommonSendImageAsync(qqNumber, null, urls);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到临时会话
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="groupNumber">目标QQ号所在的群号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<string[]> SendImageToTempAsync(long qqNumber, long groupNumber, string[] urls)
        {
            return CommonSendImageAsync(qqNumber, groupNumber, urls);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到群
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="BotMutedException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="groupNumber">目标群号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls)
        {
            return CommonSendImageAsync(null, groupNumber, urls);
        }

        private async Task<ImageMessage> InternalUploadPictureAsync(PictureTarget type, Stream imgStream)
        {
            if (ApiVersion <= new Version(1, 7, 0))
            {
                Guid guid = Guid.NewGuid();
                ImageHttpListener.RegisterImage(guid, imgStream);
                return new ImageMessage(null, $"http://127.0.0.1:{ImageHttpListener.Port}/fetch?guid={guid:n}", null);
            }
            HttpContent sessionKeyContent = new StringContent(SessionInfo.SessionKey);
            sessionKeyContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "sessionKey"
            };
            HttpContent typeContent = new StringContent(type.ToString().ToLower());
            typeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "type"
            };
            MediaTypeHeaderValue imgContentType;
            using (Image img = Image.FromStream(imgStream))
            {
                switch (img.RawFormat.ToString())
                {
                    case nameof(ImageFormat.Jpeg):
                        {
                            imgContentType = new MediaTypeHeaderValue("image/jpeg");
                            break;
                        }
                    case nameof(ImageFormat.Png):
                        {
                            imgContentType = new MediaTypeHeaderValue("image/png");
                            break;
                        }
                    case nameof(ImageFormat.Gif):
                        {
                            imgContentType = new MediaTypeHeaderValue("image/gif");
                            break;
                        }
                    default: // 不是以上三种类型的图片就强转为Png
                        {
                            MemoryStream ms = new MemoryStream();
                            img.Save(ms, ImageFormat.Png);
                            imgStream.Dispose();
                            imgStream = ms;
                            goto case nameof(ImageFormat.Png);
                        }
                }
            }
            imgStream.Seek(0, SeekOrigin.Begin);
            HttpContent imageContent = new StreamContent(imgStream);
            imageContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "img"
            };
            imageContent.Headers.ContentType = imgContentType;
            HttpContent[] contents = new HttpContent[]
            {
                sessionKeyContent,
                typeContent,
                imageContent
            };
            try
            {
                using JsonDocument j = await HttpHelper.HttpPostAsync($"{SessionInfo.Options.BaseUrl}/uploadImage", contents);
                JsonElement root = j.RootElement;
                int code = root.GetProperty("code").GetInt32();
                if (code == 0)
                {
                    return Utils.Deserialize<ImageMessage>(in root);
                }
                return ThrowCommonException<ImageMessage>(code, in root);
            }
            catch (JsonException) // https://github.com/mamoe/mirai-api-http/issues/85
            {
                throw new NotSupportedException("当前版本的mirai-api-http无法发送图片。");
            }
        }
        /// <summary>
        /// 异步上传图片
        /// <para>
        /// 注意: 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// </para>
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="imagePath">图片路径</param>
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, string imagePath)
        {
            CheckConnected();
            return InternalUploadPictureAsync(type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
        /// <summary>
        /// 异步上传图片
        /// <para>
        /// 注意: 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// </para>
        /// </summary>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="image">图片流</param>
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, Stream image)
        {
            CheckConnected();
            return InternalUploadPictureAsync(type, image);
        }
        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">消息Id, 应为 <see cref="SourceMessage.Id"/>
        /// <para>
        /// - 或 -
        /// </para>
        /// <see cref="SendFriendMessageAsync(long, IMessageBase[], int?)"/> 的返回值
        /// <para>
        /// - 或 -
        /// </para>
        /// <see cref="SendTempMessageAsync(long, long, IMessageBase[], int?)"/> 的返回值
        /// <para>
        /// - 或 -
        /// </para>
        /// <see cref="SendGroupMessageAsync(long, IMessageBase[], int?)"/> 的返回值
        /// </param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        public Task RevokeMessageAsync(int messageId)
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                target = messageId
            });
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/recall", payload, SessionInfo.Canceller.Token);
        }
    }
}
