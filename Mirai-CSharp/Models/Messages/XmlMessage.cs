using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示xml消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class XmlMessage : Messages
    {
        public const string MsgType = "Xml";
        /// <summary>
        /// Xml原始字符串
        /// </summary>
        [JsonPropertyName("xml")]
        public string Xml { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="XmlMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public XmlMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="XmlMessage"/> 类的新实例
        /// </summary>
        /// <param name="xml">要发送的原始xml字符串</param>
        public XmlMessage(string xml) : this()
        {
            Xml = xml;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:service:60,{Xml}]"; // Xml的ServiceId=60, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109
    }
}
