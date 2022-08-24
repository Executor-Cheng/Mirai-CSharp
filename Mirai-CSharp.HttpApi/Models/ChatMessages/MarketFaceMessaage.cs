using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedMarketFaceMessage = Mirai.CSharp.Models.ChatMessages.IMarketFaceMessage;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedMarketFaceMessage"/>
    [MappableMiraiChatMessageKey(MarketFaceMessage.MsgType)]
    public interface IMarketFaceMessage : ISharedMarketFaceMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedMarketFaceMessage.Id"/>
        [JsonPropertyName("id")]
        new int Id { get; }
        /// <inheritdoc cref="ISharedMarketFaceMessage.Name"/>
        [JsonPropertyName("name")]
        new string Name { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        abstract int ISharedMarketFaceMessage.Id { get; }
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        abstract string ISharedMarketFaceMessage.Name { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class MarketFaceMessage : ChatMessage, IMarketFaceMessage
    {
        public const string MsgType = "MarketFace";
        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public virtual int Id { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        public virtual string Name { get; set; }
        /// <summary>
        /// 初始化 <see cref="MarketFaceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("此类不应由用户主动创建实例。")]
        public MarketFaceMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="MarketFaceMessage"/> 类的新实例
        /// </summary>
        /// <param name="name">表情显示名称</param>
        [Obsolete("此类不应由用户主动创建实例。")]
        public MarketFaceMessage(string name)
        {
            Name = name;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:marketface:{Id},{Name}]";
#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        int ISharedMarketFaceMessage.Id => Id;
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        string ISharedMarketFaceMessage.Name => Name;
#endif
    }
}
