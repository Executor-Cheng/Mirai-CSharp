using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供撤回消息的通用信息接口
    /// </summary>
    public interface IMessageRevokedEventArgs
    {
        /// <summary>
        /// 原消息发送者的QQ号
        /// </summary>
        [JsonPropertyName("authorId")]
        long SenderId { get; }
        /// <summary>
        /// 原消息发送时间
        /// </summary>
        [JsonPropertyName("messageId")]
        int MessageId { get; }
        /// <summary>
        /// 原消息发送时间
        /// </summary>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        DateTime SentTime { get; }
    }

    public abstract class MessageRevokedEventArgs : IMessageRevokedEventArgs
    {
        /// <summary>
        /// 原消息发送者的QQ号
        /// </summary>
        [JsonPropertyName("authorId")]
        public long SenderId { get; set; }
        /// <summary>
        /// 原消息发送时间
        /// </summary>
        [JsonPropertyName("messageId")]
        public int MessageId { get; set; }
        /// <summary>
        /// 原消息发送时间
        /// </summary>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public DateTime SentTime { get; set; }

        protected MessageRevokedEventArgs() { }

        protected MessageRevokedEventArgs(long senderId, int messageId, DateTime sentTime)
        {
            SenderId = senderId;
            MessageId = messageId;
            SentTime = sentTime;
        }
    }

}
