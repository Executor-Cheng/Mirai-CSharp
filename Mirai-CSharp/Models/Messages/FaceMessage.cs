using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示一个QQ表情
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class FaceMessage : Messages
    {
        public const string MsgType = "Face";
        /// <summary>
        /// QQ表情编号, 可选, 优先高于 <see cref="Name"/>
        /// </summary>
        /// <remarks>
        /// 编号详见 <a href="https://github.com/mamoe/mirai/blob/master/mirai-core/src/commonMain/kotlin/net.mamoe.mirai/message/data/Face.kt#L41"/>
        /// </remarks>
        [JsonPropertyName("faceId")]
        public int Id { get; set; }
        /// <summary>
        /// QQ表情拼音, 可选
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        /// <summary>
        /// 初始化 <see cref="FaceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FaceMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="FaceMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">QQ表情编号, 优先高于 <paramref name="name"/></param>
        /// <param name="name">QQ表情拼音, 可选</param>
        public FaceMessage(int id, string? name) : this()
        {
            Id = id;
            Name = name;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:face:{Id}]";
    }
}
