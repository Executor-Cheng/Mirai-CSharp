namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示语音消息的基接口
    /// </summary>
    public interface IVoiceMessage : IChatMessage
    {
        /// <summary>
        /// 语音文件名
        /// </summary>
        /// <remarks>
        /// 此属性不为 <see langword="null"/> 时将忽略 <see cref="Url"/>
        /// </remarks>
        string? VoiceId { get; }
        /// <summary>
        /// 用于下载语音的Url
        /// </summary>
        /// <remarks>
        /// 此属性不为 <see langword="null"/> 时将忽略 <see cref="Path"/>
        /// </remarks>
        string? Url { get; }
        /// <summary>
        /// 语音文件的路径
        /// </summary>
        string? Path { get; }
    }
}
