using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedFaceMessage = Mirai.CSharp.Models.ChatMessages.IFaceMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedFaceMessage"/>
    [MappableMiraiChatMessageKey(FaceMessage.MsgType)]
    public interface IFaceMessage : ISharedFaceMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedFaceMessage.Id"/>
        [JsonPropertyName("faceId")]
        new int Id { get; }
        /// <inheritdoc cref="ISharedFaceMessage.Name"/>
        [JsonPropertyName("name")]
        new string? Name { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("faceId")]
        abstract int ISharedFaceMessage.Id { get; }
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        abstract string? ISharedFaceMessage.Name { get; }
#endif
    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class FaceMessage : ChatMessage, IFaceMessage
    {
        public const string MsgType = "Face";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("faceId")]
        public virtual int Id { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        public virtual string? Name { get; set; }
        /// <summary>
        /// 初始化 <see cref="FaceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public FaceMessage() { }
        /// <summary>
        /// 初始化 <see cref="FaceMessage"/> 类的新实例
        /// </summary>
        /// <param name="id">QQ表情编号, 优先高于 <paramref name="name"/></param>
        /// <param name="name">QQ表情拼音, 可选</param>
        public FaceMessage(int id, string? name)
        {
            Id = id;
            Name = name;
        }
        /// <inheritdoc/>
        public override string ToString()
            => $"[mirai:face:{Id}]";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("faceId")]
        int ISharedFaceMessage.Id => Id;
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        string? ISharedFaceMessage.Name => Name;
#endif
    }
}
