using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedXmlMessage = Mirai.CSharp.Models.ChatMessages.IXmlMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedXmlMessage"/>
    [MappableMiraiChatMessageKey(XmlMessage.MsgType)]
    public interface IXmlMessage : ISharedXmlMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedXmlMessage.Xml"/>
        [JsonPropertyName("xml")]
        new string Xml { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("xml")]
        abstract string ISharedXmlMessage.Xml { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class XmlMessage : ChatMessage, IXmlMessage
    {
        public const string MsgType = "Xml";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("xml")]
        public virtual string Xml { get; set; } = null!;
        /// <inheritdoc/>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public XmlMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="XmlMessage"/> 类的新实例
        /// </summary>
        /// <param name="xml">要发送的原始xml字符串</param>
        public XmlMessage(string xml)
        {
            Xml = xml;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:service:60,{Xml}]"; // Xml的ServiceId=60, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("xml")]
        string ISharedXmlMessage.Xml => Xml;
#endif
    }
}
