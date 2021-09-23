namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群文件信息的接口
    /// </summary>
    public interface IGroupFileInfo
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 对象Id
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 对象路径
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 父对象。不存在时为 <see langword="null"/>
        /// </summary>
        IGroupFileInfo? Parent { get; }

        /// <summary>
        /// 对象所在群信息
        /// </summary>
        IGroupInfo Group { get; }

        /// <summary>
        /// 对象是否为文件
        /// </summary>
        bool IsFile { get; }

        /// <summary>
        /// 对象是否为文件夹
        /// </summary>
        bool IsDirectory { get; }

        /// <summary>
        /// 对象下载信息
        /// </summary>
        IGroupFileDownloadInfo? DownloadInfo { get; }
    }
}
