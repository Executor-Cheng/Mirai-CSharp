using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{

    public interface IFriendMessageRevokeEventArgs : IMessageRevokedEventArgs
    {
        /// <summary>
        /// 进行撤回操作的QQ号
        /// </summary>
        [JsonPropertyName("operator")]
        long Operator { get; }
    }

    public class FriendMessageRevokedEventArgs : MessageRevokedEventArgs, IFriendMessageRevokeEventArgs
    {
        /// <summary>
        /// 进行撤回操作的QQ号
        /// </summary>
        [JsonPropertyName("operator")]
        public long Operator { get; set; }

        public FriendMessageRevokedEventArgs() { }

        public FriendMessageRevokedEventArgs(long @operator, long senderId, int messageId, DateTime sentTime) : base(senderId, messageId, sentTime)
        {
            Operator = @operator;
        }
    }

}
