using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedDiceMessage = Mirai.CSharp.Models.ChatMessages.IDiceMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedDiceMessage"/>
    [MappableMiraiChatMessageKey(DiceMessage.MsgType)]
    public interface IDiceMessage : ISharedDiceMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedDiceMessage.Point"/>
        [JsonPropertyName("value")]
        new int Point { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("value")]
        abstract int ISharedDiceMessage.Point { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class DiceMessage : ChatMessage, IDiceMessage
    {
        public const string MsgType = "Dice";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;

        /// <inheritdoc/>
        [JsonPropertyName("value")]
        public int Point { get; set; }

        /// <inheritdoc cref="DiceMessage(int)"/>
        public DiceMessage()
        {

        }

        /// <summary>
        /// 初始化 <see cref="DiceMessage"/> 类的新实例
        /// </summary>
        /// <param name="point">点数</param>
        public DiceMessage(int point)
        {
            Point = point;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("value")]
        int ISharedDiceMessage.Point => Point;
#endif
    }
}
