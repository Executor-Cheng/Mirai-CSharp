using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedFriendMessageRevokedEventArgs = Mirai.CSharp.Models.EventArgs.IFriendMessageRevokedEventArgs;
using ISharedJsonFriendMessageRevokedEventArgs = Mirai.CSharp.Models.EventArgs.IFriendMessageRevokedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友撤回消息的相关信息接口。继承自 <see cref="ISharedJsonFriendMessageRevokedEventArgs"/> 和 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("FriendRecallEvent")]
    public interface IFriendMessageRevokedEventArgs : ISharedJsonFriendMessageRevokedEventArgs, IMessageRevokedEventArgs
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedFriendMessageRevokedEventArgs.Operator"/>
        [JsonPropertyName("operator")]
        new long Operator { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("operator")]
        abstract long ISharedFriendMessageRevokedEventArgs.Operator { get; }
#endif
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

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("operator")]
        long ISharedFriendMessageRevokedEventArgs.Operator => Operator;
#endif
    }
}
