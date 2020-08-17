using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示Json消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class JsonMessage : Messages
    {
        public const string MsgType = "Json";
        /// <summary>
        /// Json原始字符串
        /// </summary>
        [JsonPropertyName("json")]
        public string Json { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="JsonMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public JsonMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="JsonMessage"/> 类的新实例
        /// </summary>
        /// <param name="json">要发送的原始Json字符串</param>
        public JsonMessage(string json) : this()
        {
            Json = json;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:service:1,{Json}]"; // Json的ServiceId=1, https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/RichMessage.kt#L109
    }
}
