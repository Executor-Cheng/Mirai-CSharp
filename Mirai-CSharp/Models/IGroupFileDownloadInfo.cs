namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群文件下载信息的接口
    /// </summary>
    public interface IGroupFileDownloadInfo
    {
        /// <summary>
        /// 对象 Sha1 校验值
        /// </summary>
        string Sha1 { get; }

        /// <summary>
        /// 对象 Md5 哈希值
        /// </summary>
        string Md5 { get; }

        /// <summary>
        /// 对象下载链接
        /// </summary>
        string? Url { get; }
    }
}
