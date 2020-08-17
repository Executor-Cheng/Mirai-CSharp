using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示App消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AppMessage : Messages
    {
        public const string MsgType = "App";
        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="AppMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public AppMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="AppMessage"/> 类的新实例
        /// </summary>
        /// <param name="content">消息内容</param>
        public AppMessage(string content) : this()
        {
            Content = content;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:app:{Content}]";
    }
}
