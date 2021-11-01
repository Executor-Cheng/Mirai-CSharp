using System;

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

        /// <summary>
        /// 对象被下载次数
        /// </summary>
        int DownloadedTimes { get; }

        /// <summary>
        /// 对象创建时间
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 对象修改时间
        /// </summary>
        DateTime ModifyTime { get; }
    }
}
