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
using System.Linq;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private static readonly JsonSerializerOptions _forSendMsg = CreateSendMsgOpt();
        
        private static JsonSerializerOptions CreateSendMsgOpt()
        {
            JsonSerializerOptions opts = JsonSerializeOptionsFactory.IgnoreNulls;
            opts.Converters.Add(new IMessageBaseArrayConverter());
            return opts;
        }

        private async Task<int> CommonSendMessageAsync(string action, long? qqNumber, long? fromGroup, IMessageBase[] chain, int? quoteMsgId)
        {
            InternalSessionInfo session = SafeGetSession();
            if (chain == null || chain.Length == 0)
            {
                throw new ArgumentException("消息链必须为非空且至少有1条消息。");
            }
            if (chain.OfType<SourceMessage>().Any())
            {
                throw new ArgumentException("无法发送基本信息(SourceMessage)。");
            }
            if (chain.OfType<QuoteMessage>().Any())
            {
                throw new ArgumentException("无法发送引用信息(QuoteMessage), 请使用quoteMsgId参数进行引用。");
            }
            if (chain.All(p => p is PlainMessage pm && string.IsNullOrEmpty(pm.Message)))
            {
                throw new ArgumentException("消息链中的所有消息均为空。");
            }
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = session.SessionKey,
                qq = qqNumber,
                group = fromGroup,
                quote = quoteMsgId,
                messageChain = chain
            }, _forSendMsg);
            using JsonDocument j = await HttpHelper.HttpPostAsync($"{session.Options.BaseUrl}/{action}", payload).GetJsonAsync(token: session.Token);
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

        private async Task<string[]> CommonSendImageAsync(long? qqNumber, long? groupNumber, string[] urls)
        {
            InternalSessionInfo session = SafeGetSession();
            if (urls == null || urls.Length == 0)
            {
                throw new ArgumentException("urls必须为非空且至少有1条url。");
            }
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = session.SessionKey,
                qq = qqNumber,
                group = groupNumber,
                urls
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            using JsonDocument j = await HttpHelper.HttpPostAsync($"{session.Options.BaseUrl}/sendImageMessage", payload).GetJsonAsync(token: session.Token);
            JsonElement root = j.RootElement;
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("code", out JsonElement code)) // 正常返回是没有code的
            {
                return ThrowCommonException<string[]>(code.GetInt32(), in root);
            }
            return Utils.Deserialize<string[]>(in root);
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

        private async Task<ImageMessage> InternalUploadPictureAsync(InternalSessionInfo session, PictureTarget type, Stream imgStream)
        {
            if (session.ApiVersion <= new Version(1, 7, 0))
            {
                Guid guid = Guid.NewGuid();
                ImageHttpListener.RegisterImage(guid, imgStream);
                return new ImageMessage(null, $"http://127.0.0.1:{ImageHttpListener.Port}/fetch?guid={guid:n}", null);
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
            using (Image img = Image.FromStream(imgStream))
            {
                format = img.RawFormat.ToString();
                switch (format)
                {
                    case nameof(ImageFormat.Jpeg):
                    case nameof(ImageFormat.Png):
                    case nameof(ImageFormat.Gif):
                        {
                            format = format.ToLower();
                            break;
                        }
                    default: // 不是以上三种类型的图片就强转为Png
                        {
                            MemoryStream ms = new MemoryStream();
                            img.Save(ms, ImageFormat.Png);
                            imgStream.Dispose();
                            imgStream = ms;
                            format = "png";
                            break;
                        }
                }
            }
            imgStream.Seek(0, SeekOrigin.Begin);
            HttpContent imageContent = new StreamContent(imgStream);
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
                using JsonDocument j = await HttpHelper.HttpPostAsync($"{session.Options.BaseUrl}/uploadImage", contents).GetJsonAsync(token: session.Token);
                JsonElement root = j.RootElement;
                if (root.TryGetProperty("code", out JsonElement code)) // 正常返回是没有code的
                {
                    return ThrowCommonException<ImageMessage>(code.GetInt32(), in root);
                }
                return Utils.Deserialize<ImageMessage>(in root);

            }
            catch (JsonException) // https://github.com/mamoe/mirai-api-http/issues/85
            {
                throw new NotSupportedException("当前版本的mirai-api-http无法发送图片。");
            }
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <remarks>
        /// 注意: 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="imagePath">图片路径</param>
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, string imagePath)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <remarks>
        /// 注意: 当 mirai-api-http 的版本小于等于v1.7.0时, 本方法返回的将是一个只有 Url 有值的 <see cref="ImageMessage"/>
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="type">目标类型</param>
        /// <param name="image">图片流</param>
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, Stream image)
        {
            InternalSessionInfo session = SafeGetSession();
            return InternalUploadPictureAsync(session, type, image);
        }
        /// <summary>
        /// 异步撤回消息
        /// </summary>
        /// <param name="messageId">
        /// 请提供以下之一
        /// <list type="bullet">
        /// <item><see cref="SourceMessage.Id"/></item>
        /// <item><see cref="SendFriendMessageAsync(long, IMessageBase[], int?)"/> 的返回值</item>
        /// <item><see cref="SendTempMessageAsync(long, long, IMessageBase[], int?)"/> 的返回值</item>
        /// <item><see cref="SendGroupMessageAsync(long, IMessageBase[], int?)"/> 的返回值</item>
        /// </list>
        /// </param>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        public Task RevokeMessageAsync(int messageId)
        {
            InternalSessionInfo session = SafeGetSession();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = session.SessionKey,
                target = messageId
            });
            return InternalHttpPostAsync($"{session.Options.BaseUrl}/recall", payload, session.Token);
        }
    }
}
