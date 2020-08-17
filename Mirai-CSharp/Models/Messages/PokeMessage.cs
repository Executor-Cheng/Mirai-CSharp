using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示戳一戳消息
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class PokeMessage : Messages
    {
        public const string MsgType = "Poke";
        /// <summary>
        /// 戳一戳的类型。
        /// </summary>
        /// <remarks>
        /// SVIP的Poke带Id, <see langword="enum"/> 无法表示两个值, 不写。
        /// 详见 <a href="https://github.com/mamoe/mirai/blob/8ca4357eb834f3c284deb68a6dd25d5c59957a82/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/HummerMessage.kt#L56"/>
        /// </remarks>
        public enum PokeType
        {
            /// <summary>
            /// 戳一戳
            /// </summary>
            Poke = 1,
            /// <summary>
            /// 比心
            /// </summary>
            ShowLove,
            /// <summary>
            /// 点赞
            /// </summary>
            Like,
            /// <summary>
            /// 心碎
            /// </summary>
            Heartbroken,
            /// <summary>
            /// 666
            /// </summary>
            SixSixSix,
            /// <summary>
            /// 放大招
            /// </summary>
            FangDaZhao,
        }
        /// <summary>
        /// 戳一戳类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("name")]
        public PokeType Name { get; set; }
        /// <summary>
        /// 初始化 <see cref="PokeMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PokeMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="PokeMessage"/> 类的新实例
        /// </summary>
        /// <param name="name">戳一戳的类型</param>
        public PokeMessage(PokeType name) : this()
        {
            Name = name;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:poke:{(int)Name},-1]"; // id在PokeType∈[1,6]时固定为-1
    }
}
