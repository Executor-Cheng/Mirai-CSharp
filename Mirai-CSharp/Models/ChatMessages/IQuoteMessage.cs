namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示引用消息的基接口
    /// </summary>
    public interface IQuoteMessage : IChatMessage
    {
        /// <summary>
        /// 被引用回复的原消息的messageId
        /// </summary>
        int Id { get; }
        /// <summary>
        /// 被引用回复的原消息所接收的群号, 当为好友消息时为0
        /// </summary>
        long GroupId { get; }
        /// <summary>
        /// 被引用回复的原消息的发送者的QQ号
        /// </summary>
        long SenderId { get; }
        /// <summary>
        /// 被引用回复的原消息的接收者者的QQ号（或群号）
        /// </summary>
        long TargetId { get; }
        /// <summary>
        /// 被引用回复的原消息的消息链数组
        /// </summary>
        IChatMessage[]? OriginChain { get; }
    }
}
