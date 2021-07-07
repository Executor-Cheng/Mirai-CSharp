using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供好友撤回消息的相关信息接口。继承自 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    public interface IFriendMessageRevokedEventArgs : IMessageRevokedEventArgs
    {
        /// <summary>
        /// 进行撤回操作的QQ号
        /// </summary>
        [JsonPropertyName("operator")]
        long Operator { get; }
    }

    public class FriendMessageRevokedEventArgs : MessageRevokedEventArgs, IFriendMessageRevokedEventArgs
    {
        /// <summary>
        /// 进行撤回操作的QQ号
        /// </summary>
        [JsonPropertyName("operator")]
        public long Operator { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageRevokedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageRevokedEventArgs(long @operator, long senderId, int messageId, DateTime sentTime) : base(senderId, messageId, sentTime)
        {
            Operator = @operator;
        }
    }

}
