namespace Mirai.CSharp.Models.ChatMessages
{
    /// <summary>
    /// 表示分享音乐消息的基接口
    /// </summary>
    public interface ISharedMusicMessage : IChatMessage
    {
        /// <summary>
        /// 类型
        /// </summary>
        string Kind { get; }

        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 简述
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        string JumpUrl { get; }

        /// <summary>
        /// 封面图片链接
        /// </summary>
        string PictureUrl { get; }

        /// <summary>
        /// 音源链接
        /// </summary>
        string MusicUrl { get; }

        /// <summary>
        /// 简介
        /// </summary>
        string Brief { get; }
    }
}
