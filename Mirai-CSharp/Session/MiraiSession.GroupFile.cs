using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task<IGroupFileInfo[]> GetFilelistAsync(long groupNumber, string? id, bool fetchDownloadInfo, int offset, int size, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<IGroupFileInfo[]> GetFilelistAsync(long groupNumber, IGroupFileInfo? directory, bool fetchDownloadInfo, int offset, int size, CancellationToken token = default)
        {
            return GetFilelistAsync(groupNumber, directory?.Id, fetchDownloadInfo, offset, size, token);
        }

        /// <inheritdoc/>
        public abstract Task<IGroupFileInfo> GetFileInfoAsync(long groupNumber, string id, bool fetchDownloadInfo, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<IGroupFileInfo> GetFileInfoAsync(long groupNumber, IGroupFileInfo file, bool fetchDownloadInfo, CancellationToken token = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            return GetFileInfoAsync(groupNumber, file.Id, fetchDownloadInfo, token);
        }

        /// <inheritdoc/>
        public abstract Task CreateDirectoryAsync(long groupNumber, string? id, string directoryName, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task CreateDirectoryAsync(long groupNumber, IGroupFileInfo? directory, string directoryName, CancellationToken token = default)
        {
            return CreateDirectoryAsync(groupNumber, directory?.Id, directoryName, token);
        }

        /// <inheritdoc/>
        public abstract Task DeleteFileAsync(long groupNumber, string id, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task DeleteFileAsync(long groupNumber, IGroupFileInfo file, CancellationToken token = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            return DeleteFileAsync(groupNumber, file.Id, token);
        }

        /// <inheritdoc/>
        public abstract Task MoveFileAsync(long groupNumber, string srcId, string? dstId, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task MoveFileAsync(long groupNumber, IGroupFileInfo src, IGroupFileInfo? dst, CancellationToken token = default)
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }
            return MoveFileAsync(groupNumber, src.Id, dst?.Id, token);
        }

        /// <inheritdoc/>
        public abstract Task RenameFileAsync(long groupNumber, string id, string renameTo, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task RenameFileAsync(long groupNumber, IGroupFileInfo file, string renameTo, CancellationToken token = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            return RenameFileAsync(groupNumber, file.Id, renameTo, token);
        }

        /// <inheritdoc/>
        public virtual Task<IGroupFileInfo> UploadFileAsync(string? id, byte[] data, CancellationToken token = default)
        {
            MemoryStream ms = new MemoryStream(data);
            return UploadFileAsync(id, ms, token).DisposeWhenCompleted(ms);
        }

        /// <inheritdoc/>
        public virtual Task<IGroupFileInfo> UploadFileAsync(IGroupFileInfo? directory, byte[] data, CancellationToken token = default)
        {
            MemoryStream ms = new MemoryStream(data);
            return UploadFileAsync(directory?.Id, ms, token).DisposeWhenCompleted(ms);
        }

        /// <inheritdoc/>
        public abstract Task<IGroupFileInfo> UploadFileAsync(string? id, Stream fileStream, CancellationToken token = default);

        public abstract Task<IGroupFileInfo> UploadFileAsync(string? id, long groupNumber, string FilePath, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task<IGroupFileInfo> UploadFileAsync(IGroupFileInfo? directory, Stream fileStream, CancellationToken token = default)
        {
            return UploadFileAsync(directory?.Id, fileStream, token);
        }
    }
}
