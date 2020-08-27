using System;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示语音消息。
    /// </summary>
    public class VoiceMessage : Messages
    {
        public const string MsgType = "Voice";
        /// <summary>
        /// 语音文件名
        /// </summary>
        /// <remarks>
        /// 此属性不为 <see langword="null"/> 时将忽略 <see cref="Url"/>
        /// </remarks>
        [JsonPropertyName("voiceId")]
        public string? VoiceId { get; set; } = null!;
        /// <summary>
        /// 用于下载语音的Url
        /// </summary>
        /// <remarks>
        /// 此属性不为 <see langword="null"/> 时将忽略 <see cref="Path"/>
        /// </remarks>
        [JsonPropertyName("url")]
        public string? Url { get; set; } = null!;
        /// <summary>
        /// 语音文件的路径
        /// </summary>
        [JsonPropertyName("path")]
        public string? Path { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public VoiceMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        /// <param name="voiceId">语音Id</param>
        /// <param name="url">用于下载语音的Url</param>
        /// <param name="path">语音文件的路径, 相对路径于 plugins/MiraiAPIHTTP/voices</param>
        public VoiceMessage(string? voiceId, string? url, string? path) : this()
        {
            VoiceId = voiceId;
            Url = url;
            Path = path;
        }
    }
}
