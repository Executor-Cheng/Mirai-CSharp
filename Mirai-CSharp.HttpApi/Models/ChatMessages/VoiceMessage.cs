using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedVoiceMessage = Mirai.CSharp.Models.ChatMessages.IVoiceMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedVoiceMessage"/>
    [MappableMiraiChatMessageKey(VoiceMessage.MsgType)]
    public interface IVoiceMessage : ISharedVoiceMessage, IChatMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedVoiceMessage.VoiceId"/>
        [JsonPropertyName("voiceId")]
        new string? VoiceId { get; }
        
        /// <inheritdoc cref="ISharedVoiceMessage.Url"/>
        [JsonPropertyName("url")]
        new string? Url { get; }
        
        /// <inheritdoc cref="ISharedVoiceMessage.Path"/>
        [JsonPropertyName("path")]
        new string? Path { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("voiceId")]
        abstract string? ISharedVoiceMessage.VoiceId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("url")]
        abstract string? ISharedVoiceMessage.Url { get; }

        /// <inheritdoc/>
        [JsonPropertyName("path")]
        abstract string? ISharedVoiceMessage.Path { get; }
#endif
    }

    public class VoiceMessage : ChatMessage, IVoiceMessage
    {
        public const string MsgType = "Voice";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <inheritdoc/>
        [JsonPropertyName("voiceId")]
        public virtual string? VoiceId { get; set; } = null!;
        /// <inheritdoc/>
        [JsonPropertyName("url")]
        public virtual string? Url { get; set; } = null!;
        /// <inheritdoc/>
        [JsonPropertyName("path")]
        public virtual string? Path { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public VoiceMessage()
        {

        }
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        /// <param name="voiceId">语音Id</param>
        /// <param name="url">用于下载语音的Url</param>
        /// <param name="path">语音文件的路径, 相对路径于 plugins/MiraiAPIHTTP/voices</param>
        public VoiceMessage(string? voiceId, string? url, string? path)
        {
            VoiceId = voiceId;
            Url = url;
            Path = path;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("voiceId")]
        string? ISharedVoiceMessage.VoiceId => VoiceId;
        
        /// <inheritdoc/>
        [JsonPropertyName("url")]
        string? ISharedVoiceMessage.Url => Url;
        
        /// <inheritdoc/>
        [JsonPropertyName("path")]
        string? ISharedVoiceMessage.Path => Path;
#endif
    }
}
