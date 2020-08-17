using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示 @特定对象 消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class AtMessage : Messages
    {
        public const string MsgType = "At";
        /// <summary>
        /// 被@的群员QQ号
        /// </summary>
        [JsonPropertyName("target")]
        public long Target { get; set; }
        /// <summary>
        /// At时显示的文字, 发送消息时无效, 自动使用群名片
        /// </summary>
        [JsonPropertyName("display")]
        public string Display { get; set; } = null!; // 由反序列化赋值
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        /// <param name="target">要@的群员QQ号</param>
        public AtMessage(long target) : this()
        {
            Target = target;
        }
        /// <summary>
        /// 初始化 <see cref="AtMessage"/> 类的新实例
        /// </summary>
        /// <param name="target">要@的群员QQ号</param>
        /// <param name="display">@时显示的文字</param>
        [Obsolete("请使用AtMessage(long)构造方法初始化本类实例。")]
        public AtMessage(long target, string display) : this(target)
        {
            Display = display;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:at:{Target}]";
    }
}
