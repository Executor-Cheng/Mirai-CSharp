using System;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 此警告是用户专用的
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 表示语音消息。
    /// </summary>
    /// <remarks>
    /// 提前实现。mirai-api-http不支持传递此消息。
    /// </remarks>
    [Obsolete("mirai-api-http不支持传递此消息。")]
    public class VoiceMessage : Messages
    {
        public const string MsgType = "Voice";
        /// <summary>
        /// 语音文件名
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = null!;
        /// <summary>
        /// 语音文件的md5值
        /// </summary>
        /// <remarks>
        /// 个人推测
        /// </remarks>
        [JsonPropertyName("md5")]
        public string Md5 { get; set; } = null!;
        /// <summary>
        /// 用于下载语音的Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public VoiceMessage() : base(MsgType) { }
        /// <summary>
        /// 初始化 <see cref="VoiceMessage"/> 类的新实例
        /// </summary>
        /// <param name="fileName">语音文件名</param>
        /// <param name="md5">语音文件的md5值 (个人推测)</param>
        /// <param name="url">用于下载语音的Url</param>
        public VoiceMessage(string fileName, string md5, string url) : this()
        {
            FileName = fileName;
            Md5 = md5;
            Url = url;
        }
    }
}
