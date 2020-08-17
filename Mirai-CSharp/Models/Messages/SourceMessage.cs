using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示消息的基本信息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class SourceMessage : Messages
    {
        public const string MsgType = "Source";
        /// <summary>
        /// 消息的识别号, 用于引用回复或撤回
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// 消息时间
        /// </summary>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        /// <summary>
        /// 初始化 <see cref="SourceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="SourceMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">消息的识别号, 用于引用回复或撤回</param>
        /// <param name="time">消息时间</param>
        [Obsolete("此类不应由用户主动创建实例。")]
        public SourceMessage(int id, DateTime time) : this()
        {
            Id = id;
            Time = time;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:source:{Id}]";
    }
}
