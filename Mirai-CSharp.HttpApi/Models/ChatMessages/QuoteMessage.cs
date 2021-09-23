using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
using ISharedQuoteMessage = Mirai.CSharp.Models.ChatMessages.IQuoteMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedQuoteMessage"/>
    [MappableMiraiChatMessageKey(QuoteMessage.MsgType)]
    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public interface IQuoteMessage : ISharedQuoteMessage, IChatMessage
    {
        /// <inheritdoc cref="ISharedQuoteMessage.OriginChain"/>
        [JsonPropertyName("origin")]
        new IChatMessage[]? OriginChain { get; }

#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedQuoteMessage.Id"/>
        [JsonPropertyName("id")]
        new int Id { get; }
        
        /// <inheritdoc cref="ISharedQuoteMessage.GroupId"/>
        [JsonPropertyName("groupId")]
        new long GroupId { get; }
        
        /// <inheritdoc cref="ISharedQuoteMessage.SenderId"/>
        [JsonPropertyName("senderId")]
        new long SenderId { get; }
        
        /// <inheritdoc cref="ISharedQuoteMessage.TargetId"/>
        [JsonPropertyName("targetId")]
        new long TargetId { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        abstract int ISharedQuoteMessage.Id { get; }

        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        abstract long ISharedQuoteMessage.GroupId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        abstract long ISharedQuoteMessage.SenderId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("targetId")]
        abstract long ISharedQuoteMessage.TargetId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        ISharedChatMessage[]? ISharedQuoteMessage.OriginChain => OriginChain;
#endif
    }

    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    [DebuggerDisplay("{ToString(),nq}")]
    public class QuoteMessage : ChatMessage, IQuoteMessage
    {
        public const string MsgType = "Quote";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public virtual int Id { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        public virtual long GroupId { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        public virtual long SenderId { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("targetId")]
        public virtual long TargetId { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        public virtual IChatMessage[]? OriginChain { get; set; }
        /// <inheritdoc cref="QuoteMessage(int, long, long, long, IChatMessage[])"/>
        [Obsolete("请使用 QuoteMessage(int, long, long, long) 初始化本类实例。")]
        public QuoteMessage()
        {

        }
        /// <inheritdoc cref="QuoteMessage(int, long, long, long, IChatMessage[])"/>
        public QuoteMessage(int id, long groupId, long senderId, long targetId)
        {
            Id = id;
            GroupId = groupId;
            SenderId = senderId;
            TargetId = targetId;
        }
        /// <summary>
        /// 初始化 <see cref="QuoteMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">被引用回复的原消息的messageId</param>
        /// <param name="groupId">被引用回复的原消息所接收的群号, 当为好友消息时为0</param>
        /// <param name="senderId">被引用回复的原消息的发送者的QQ号</param>
        /// <param name="targetId">被引用回复的原消息的接收者者的QQ号（或群号）</param>
        /// <param name="originChain">被引用回复的原消息的消息链数组</param>
        [Obsolete("请使用 QuoteMessage(int, long, long, long) 初始化本类实例。")]
        public QuoteMessage(int id, long groupId, long senderId, long targetId, IChatMessage[]? originChain) : this(id, groupId, senderId, targetId)
        {
            OriginChain = originChain;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:quote:{Id}]";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        int ISharedQuoteMessage.Id => Id;

        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        long ISharedQuoteMessage.GroupId => GroupId;

        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        long ISharedQuoteMessage.SenderId => SenderId;

        /// <inheritdoc/>
        [JsonPropertyName("targetId")]
        long ISharedQuoteMessage.TargetId => TargetId;

        /// <inheritdoc/>
        [JsonPropertyName("origin")]
        ISharedChatMessage[]? ISharedQuoteMessage.OriginChain => OriginChain;
#endif
    }
}
