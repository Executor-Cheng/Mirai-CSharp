using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步获取给定群内的文件列表
        /// </summary>
        /// <param name="groupNumber">群号</param>
        /// <param name="id">文件夹Id, 为 <see cref="string.Empty"/> 或 <see langword="null"/> 时将获取根目录</param>
        /// <param name="fetchDownloadInfo">是否获取下载链接</param>
        /// <param name="offset">分页偏移</param>
        /// <param name="size">分页大小</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/>, 其值为文件列表</returns>
        Task<IGroupFileInfo[]> GetFilelistAsync(long groupNumber, string? id, bool fetchDownloadInfo, int offset, int size, CancellationToken token = default);

        /// <inheritdoc cref="GetFilelistAsync(long, string?, bool, int, int, CancellationToken)"/>
        /// <param name="directory">目标目录。为 <see langword="null"/> 时将获取根目录</param>
        Task<IGroupFileInfo[]> GetFilelistAsync(long groupNumber, IGroupFileInfo? directory, bool fetchDownloadInfo, int offset, int size, CancellationToken token = default);

        /// <summary>
        /// 异步获取给定文件信息
        /// </summary>
        /// <returns>表示此异步操作的 <see cref="Task"/>, 其值为文件信息</returns>
        /// <inheritdoc cref="GetFilelistAsync(long, string?, bool, int, int, CancellationToken)"/>
        Task<IGroupFileInfo> GetFileInfoAsync(long groupNumber, string id, bool fetchDownloadInfo, CancellationToken token = default);

        /// <inheritdoc cref="GetFileInfoAsync(long, string, bool, CancellationToken)"/>
        /// <param name="file">目标文件信息</param>
        Task<IGroupFileInfo> GetFileInfoAsync(long groupNumber, IGroupFileInfo file, bool fetchDownloadInfo, CancellationToken token = default);

        /// <summary>
        /// 异步创建文件夹
        /// </summary>
        /// <param name="groupNumber">群号</param>
        /// <param name="id">父目录id。为 <see cref="string.Empty"/> 或 <see langword="null"/> 时将从根目录创建文件夹</param>
        /// <param name="directoryName">文件夹名</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task CreateDirectoryAsync(long groupNumber, string? id, string directoryName, CancellationToken token = default);

        /// <inheritdoc cref="CreateDirectoryAsync(long, string?, string, CancellationToken)"/>
        /// <param name="directory">父目录信息。为 <see langword="null"/> 时将从根目录创建文件夹</param>
        Task CreateDirectoryAsync(long groupNumber, IGroupFileInfo? directory, string directoryName, CancellationToken token = default);

        /// <summary>
        /// 异步删除给定文件
        /// </summary>
        /// <param name="groupNumber">群号</param>
        /// <param name="id">文件Id</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task DeleteFileAsync(long groupNumber, string id, CancellationToken token = default);

        /// <inheritdoc cref="DeleteFileAsync(long, string, CancellationToken)"/>
        /// <param name="file">文件信息</param>
        Task DeleteFileAsync(long groupNumber, IGroupFileInfo file, CancellationToken token = default);

        /// <summary>
        /// 异步移动给定文件至给定目录
        /// </summary>
        /// <param name="groupNumber">群号</param>
        /// <param name="srcId">源文件Id</param>
        /// <param name="dstId">目标文件夹Id。为 <see langword="null"/> 时将移动至根目录</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task MoveFileAsync(long groupNumber, string srcId, string? dstId, CancellationToken token = default);

        /// <inheritdoc cref="MoveFileAsync(long, string, string?, CancellationToken)"/>
        /// <param name="src">源文件信息</param>
        /// <param name="dst">目标文件夹信息。为 <see langword="null"/> 时将移动至根目录</param>
        Task MoveFileAsync(long groupNumber, IGroupFileInfo src, IGroupFileInfo? dst, CancellationToken token = default);

        /// <summary>
        /// 异步更名给定文件
        /// </summary>
        /// <param name="groupNumber">群号</param>
        /// <param name="id">文件Id</param>
        /// <param name="renameTo">目标名称</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task RenameFileAsync(long groupNumber, string id, string renameTo, CancellationToken token = default);

        /// <inheritdoc cref="RenameFileAsync(long, string, string, CancellationToken)"/>
        /// <param name="file">文件信息</param>
        Task RenameFileAsync(long groupNumber, IGroupFileInfo file, string renameTo, CancellationToken token = default);

        /// <summary>
        /// 异步上传文件
        /// </summary>
        /// <param name="id">目标文件夹Id。为 <see langword="null"/> 时将上传到根目录</param>
        /// <param name="data">文件二进制数组</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/>, 其值为上传后的文件信息</returns>
        Task<IGroupFileInfo> UploadFileAsync(string? id, byte[] data, CancellationToken token = default);

        /// <inheritdoc cref="UploadFileAsync(string?, byte[], CancellationToken)"/>
        /// <param name="directory">文件夹信息。为 <see langword="null"/> 时将上传到根目录</param>
        Task<IGroupFileInfo> UploadFileAsync(IGroupFileInfo? directory, byte[] data, CancellationToken token = default);

        /// <inheritdoc cref="UploadFileAsync(string?, byte[], CancellationToken)"/>
        /// <param name="fileStream">文件流。此流将会被读取至末尾</param>
        Task<IGroupFileInfo> UploadFileAsync(string? id, Stream fileStream, CancellationToken token = default);

        /// <inheritdoc cref="UploadFileAsync(IGroupFileInfo?, byte[], CancellationToken)"/>
        /// <param name="fileStream">文件流。此流将会被读取至末尾</param>
        Task<IGroupFileInfo> UploadFileAsync(IGroupFileInfo? directory, Stream fileStream, CancellationToken token = default);

        /// <inheritdoc cref="UploadFileAsync(string?, byte[], CancellationToken)"/>
        /// <param name="fileStream">文件流。此流将会被读取至末尾</param>
        Task<IGroupFileInfo> UploadFileAsync(string? id, long groupNumber, string FilePath, CancellationToken token = default);
    }
}
