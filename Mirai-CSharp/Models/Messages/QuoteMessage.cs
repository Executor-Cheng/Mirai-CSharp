using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
#pragma warning disable CA1819 // Properties should not return arrays
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示引用消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class QuoteMessage : Messages
    {
        public const string MsgType = "Quote";
        /// <summary>
        /// 被引用回复的原消息的messageId
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// 被引用回复的原消息所接收的群号, 当为好友消息时为0
        /// </summary>
        [JsonPropertyName("groupId")]
        public long GroupId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的发送者的QQ号
        /// </summary>
        [JsonPropertyName("senderId")]
        public long SenderId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的接收者者的QQ号（或群号）
        /// </summary>
        [JsonPropertyName("targetId")]
        public long TargetId { get; set; }
        /// <summary>
        /// 被引用回复的原消息的消息链数组
        /// </summary>
        [JsonConverter(typeof(IMessageBaseArrayConverter))]
        [JsonPropertyName("origin")]
        public IMessageBase[] OriginChain { get; set; } = null!; // 只在反序列化时进行赋值, 故不为null
        /// <summary>
        /// 初始化 <see cref="QuoteMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public QuoteMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="QuoteMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">被引用回复的原消息的messageId</param>
        /// <param name="groupId">被引用回复的原消息所接收的群号, 当为好友消息时为0</param>
        /// <param name="senderId">被引用回复的原消息的发送者的QQ号</param>
        /// <param name="targetId">被引用回复的原消息的接收者者的QQ号（或群号）</param>
        /// <param name="originChain">被引用回复的原消息的消息链数组</param>
        public QuoteMessage(int id, long groupId, long senderId, long targetId, IMessageBase[] originChain) : this()
        {
            Id = id;
            GroupId = groupId;
            SenderId = senderId;
            TargetId = targetId;
            OriginChain = originChain;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:quote:{Id}]";
    }
}
