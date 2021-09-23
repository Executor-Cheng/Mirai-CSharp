using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupFileDownloadInfo = Mirai.CSharp.Models.IGroupFileDownloadInfo;
using ISharedGroupFileInfo = Mirai.CSharp.Models.IGroupFileInfo;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface IGroupFileInfo : ISharedGroupFileInfo
    {
        /// <inheritdoc cref="ISharedGroupFileInfo.Parent"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileInfo, IGroupFileInfo>))]
        [JsonPropertyName("parent")]
        new IGroupFileInfo? Parent { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.Group"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("contact")]
        new IGroupInfo Group { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.DownloadInfo"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileDownloadInfo, IGroupFileDownloadInfo>))]
        [JsonPropertyName("parent")]
        new IGroupFileDownloadInfo? DownloadInfo { get; }

#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedGroupFileInfo.Name"/>
        [JsonPropertyName("name")]
        new string Name { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.Id"/>
        [JsonPropertyName("id")]
        new string Id { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.Path"/>
        [JsonPropertyName("path")]
        new string Path { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.IsFile"/>
        [JsonPropertyName("isFile")]
        new bool IsFile { get; }

        /// <inheritdoc cref="ISharedGroupFileInfo.IsDirectory"/>
        [JsonPropertyName("isDirectory")]
        new bool IsDirectory { get; }
#else
        [JsonPropertyName("name")]
        abstract string ISharedGroupFileInfo.Name { get; }

        [JsonPropertyName("id")]
        abstract string ISharedGroupFileInfo.Id { get; }

        [JsonPropertyName("path")]
        abstract string ISharedGroupFileInfo.Path { get; }

        [JsonPropertyName("isFile")]
        abstract bool ISharedGroupFileInfo.IsFile { get; }

        [JsonPropertyName("isDirectory")]
        abstract bool ISharedGroupFileInfo.IsDirectory { get; }

        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileInfo, ISharedGroupFileInfo>))]
        [JsonPropertyName("parent")]
        ISharedGroupFileInfo? ISharedGroupFileInfo.Parent => Parent;

        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("contact")]
        ISharedGroupInfo ISharedGroupFileInfo.Group => Group;

        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileDownloadInfo, ISharedGroupFileDownloadInfo>))]
        [JsonPropertyName("downloadInfo")]
        ISharedGroupFileDownloadInfo? ISharedGroupFileInfo.DownloadInfo => DownloadInfo;
#endif
    }

    public class GroupFileInfo : IGroupFileInfo
    {
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("path")]
        public string Path { get; set; } = null!;

        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileInfo, IGroupFileInfo>))]
        [JsonPropertyName("parent")]
        public IGroupFileInfo? Parent { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("contact")]
        public IGroupInfo Group { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("isFile")]
        public bool IsFile { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("isDirectory")]
        public bool IsDirectory { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileDownloadInfo, IGroupFileDownloadInfo>))]
        [JsonPropertyName("downloadInfo")]
        public IGroupFileDownloadInfo? DownloadInfo { get; set; }

        public GroupFileInfo()
        {

        }

        public GroupFileInfo(string name, string id, string path, GroupFileInfo? parent, GroupInfo group, bool isFile, bool isDirectory, GroupFileDownloadInfo? downloadInfo)
        {
            Name = name;
            Id = id;
            Path = path;
            Parent = parent;
            Group = group;
            IsFile = isFile;
            IsDirectory = isDirectory;
            DownloadInfo = downloadInfo;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileInfo, ISharedGroupFileInfo>))]
        [JsonPropertyName("parent")]
        ISharedGroupFileInfo? ISharedGroupFileInfo.Parent => Parent;

        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("contact")]
        ISharedGroupInfo ISharedGroupFileInfo.Group => Group;

        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupFileDownloadInfo, ISharedGroupFileDownloadInfo>))]
        [JsonPropertyName("downloadInfo")]
        ISharedGroupFileDownloadInfo? ISharedGroupFileInfo.DownloadInfo => DownloadInfo;
#endif
    }
}
