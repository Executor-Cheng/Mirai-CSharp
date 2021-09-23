using System.Text.Json.Serialization;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <summary>
    /// 聊天消息的基接口
    /// </summary>
    public interface IChatMessage : ISharedChatMessage, IMiraiHttpMessage
    {
        /// <summary>
        /// 消息类型。供api或反序列化使用
        /// </summary>
        [JsonPropertyName("type")]
        string Type { get; }
    }

    /// <summary>
    /// 聊天消息的基类
    /// </summary>
    public abstract class ChatMessage : MiraiHttpMessage, IChatMessage
    {
        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public abstract string Type { get; }
    }
}
