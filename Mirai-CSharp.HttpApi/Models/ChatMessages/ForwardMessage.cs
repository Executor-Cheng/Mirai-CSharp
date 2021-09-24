using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedForwardMessage = Mirai.CSharp.Models.ChatMessages.IForwardMessage;
using ISharedForwardMessageNode = Mirai.CSharp.Models.ChatMessages.IForwardMessageNode;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    [MappableMiraiChatMessageKey("Forward")]
    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public interface IForwardMessage : ISharedForwardMessage, IChatMessage
    {
        [JsonPropertyName("nodeList")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ForwardMessageNode[], IForwardMessageNode[]>))]
        new IForwardMessageNode[] Nodes { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ForwardMessageNode[], ISharedForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        ISharedForwardMessageNode[] ISharedForwardMessage.Nodes => Nodes;
#endif
    }

    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    [DebuggerDisplay("{ToString(),nq}")]
    public class ForwardMessage : ChatMessage, IForwardMessage
    {
        public const string MsgType = "Forward";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;

        /// <summary>
        /// 转发的消息数组
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<ForwardMessageNode[], IForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        public IForwardMessageNode[] Nodes { get; set; } = null!;

        /// <inheritdoc cref="ForwardMessage(IForwardMessageNode[])"/>
        [Obsolete("请使用 ForwardMessage(IForwardMessageNode[]) 初始化本类实例。")]
        public ForwardMessage()
        {

        }

        /// <summary>
        /// 初始化 <see cref="ForwardMessage"/> 类的新实例
        /// </summary>
        /// <param name="nodes">转发的消息数组</param>
        public ForwardMessage(IForwardMessageNode[] nodes)
        {
            Nodes = nodes;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ForwardMessageNode[], ISharedForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        ISharedForwardMessageNode[] ISharedForwardMessage.Nodes => Nodes;
#endif
    }
}
