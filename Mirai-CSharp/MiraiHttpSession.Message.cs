using Mirai_CSharp.Helpers;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using Mirai_CSharp.Utility.JsonConverters;
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
            CheckArray(chain);
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
            CheckArray(urls);
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
        /// <param name="qqNumber">目标QQ号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<string[]> SendImageToFriendAsync(long qqNumber, string[] urls)
        {
            return CommonSendImageAsync(qqNumber, null, urls);
        }
        /// <summary>
        /// 异步发送给定Url数组中的图片到临时会话
        /// </summary>
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
        /// <param name="groupNumber">目标群号</param>
        /// <param name="urls">一个Url数组。不可为 <see langword="null"/> 或空数组</param>
        public Task<string[]> SendImageToGroupAsync(long groupNumber, string[] urls)
        {
            return CommonSendImageAsync(null, groupNumber, urls);
        }

        private async Task<ImageMessage> InternalUploadPictureAsync(PictureTarget type, Stream image)
        {
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
            HttpContent imageContent = new StreamContent(image);
            typeContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "img"
            };
            HttpContent[] contents = new HttpContent[]
            {
                sessionKeyContent,
                typeContent,
                imageContent
            };
            using JsonDocument j = await HttpHelper.HttpPostAsync($"{SessionInfo.Options.BaseUrl}/uploadImage", contents);
            JsonElement root = j.RootElement;
            int code = root.GetProperty("code").GetInt32();
            if (code == 0)
            {
                return Utils.Deserialize<ImageMessage>(in root);
            }
            return ThrowCommonException<ImageMessage>(code, in root);
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="imagePath">图片路径</param>
        public Task<ImageMessage> UploadPictureAsync(PictureTarget type, string imagePath)
        {
            CheckConnected();
            return InternalUploadPictureAsync(type, new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
        /// <summary>
        /// 异步上传图片
        /// </summary>
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
        /// <see cref="SendFriendMessageAsync(long, IMessageBase[], int?)"/>
        /// <para>
        /// - 或 -
        /// </para>
        /// <see cref="SendTempMessageAsync(long, long, IMessageBase[], int?)"/>
        /// <para>
        /// - 或 -
        /// </para>
        /// <see cref="SendGroupMessageAsync(long, IMessageBase[], int?)"/> 的返回值
        /// </param>
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
