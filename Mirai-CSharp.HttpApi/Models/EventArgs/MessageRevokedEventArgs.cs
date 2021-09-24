using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedJsonElementMessageRevokedEventArgs = Mirai.CSharp.Models.EventArgs.IMessageRevokedEventArgs<System.Text.Json.JsonElement>;
using ISharedMessageRevokedEventArgs = Mirai.CSharp.Models.EventArgs.IMessageRevokedEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供撤回消息的通用信息接口。继承自 <see cref="ISharedJsonElementMessageRevokedEventArgs"/> 和 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    public interface IMessageRevokedEventArgs : ISharedJsonElementMessageRevokedEventArgs, IMiraiHttpMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedMessageRevokedEventArgs.SenderId"/>
        [JsonPropertyName("authorId")]
        new long SenderId { get; }
        /// <inheritdoc cref="ISharedMessageRevokedEventArgs.MessageId"/>
        [JsonPropertyName("messageId")]
        new int MessageId { get; }
        /// <inheritdoc cref="ISharedMessageRevokedEventArgs.SentTime"/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        new DateTime SentTime { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("authorId")]
        abstract long ISharedMessageRevokedEventArgs.SenderId { get; }
        /// <inheritdoc/>
        [JsonPropertyName("messageId")]
        abstract int ISharedMessageRevokedEventArgs.MessageId { get; }
        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        abstract DateTime ISharedMessageRevokedEventArgs.SentTime { get; }
#endif
    }

    public abstract class MessageRevokedEventArgs : MiraiHttpMessage, IMessageRevokedEventArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("authorId")]
        public long SenderId { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("messageId")]
        public int MessageId { get; set; }
        /// <inheritdoc/>
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

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("authorId")]
        long ISharedMessageRevokedEventArgs.SenderId => SenderId;
        /// <inheritdoc/>
        [JsonPropertyName("messageId")]
        int ISharedMessageRevokedEventArgs.MessageId => MessageId;
        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        DateTime ISharedMessageRevokedEventArgs.SentTime => SentTime;
#endif
    }
}
