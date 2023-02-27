using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedForwardMessage = Mirai.CSharp.Models.ChatMessages.IForwardMessage;
using ISharedForwardMessageDisplay = Mirai.CSharp.Models.ChatMessages.IForwardMessageDisplay;
using ISharedForwardMessageNode = Mirai.CSharp.Models.ChatMessages.IForwardMessageNode;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{

    [MappableMiraiChatMessageKey("Forward")]
    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public interface IForwardMessage : ISharedForwardMessage, IChatMessage
    {
        [JsonPropertyName("nodeList")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageNode[], object[], ForwardMessageNode[]>))]
        new IForwardMessageNode[] Nodes { get; }

        [JsonPropertyName("display")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageDisplay, ForwardMessageDisplay>))]
        new IForwardMessageDisplay? Display { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageNode[], object[], ForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        ISharedForwardMessageNode[] ISharedForwardMessage.Nodes => Nodes;

        [JsonPropertyName("display")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageDisplay, ForwardMessageDisplay>))]
        ISharedForwardMessageDisplay? ISharedForwardMessage.Display => Display;
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
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageNode[], object[], ForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        public IForwardMessageNode[] Nodes { get; set; }

        /// <summary>
        /// 转发消息展示行为
        /// </summary>
        [JsonPropertyName("display")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageDisplay, ForwardMessageDisplay>))]
        public IForwardMessageDisplay? Display { get; set; }

        /// <inheritdoc cref="ForwardMessage(IForwardMessageNode[])"/>
        [Obsolete("请使用 ForwardMessage(IForwardMessageNode[]), ForwardMessage(IForwardMessageNode[], IForwardMessageDisplay?) 初始化本类实例。")]
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

        /// <summary>
        /// 初始化 <see cref="ForwardMessage"/> 类的新实例
        /// </summary>
        /// <param name="nodes">转发的消息数组</param>
        /// <param name="display">转发消息的展示行为</param>
        public ForwardMessage(IForwardMessageNode[] nodes, IForwardMessageDisplay? display)
        {
            Nodes = nodes;
            Display = display;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageNode[], object[], ForwardMessageNode[]>))]
        [JsonPropertyName("nodeList")]
        ISharedForwardMessageNode[] ISharedForwardMessage.Nodes => Nodes;

        [JsonPropertyName("display")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageDisplay, ForwardMessageDisplay>))]
        ISharedForwardMessageDisplay? ISharedForwardMessage.Display => Display;
#endif
    }
}
