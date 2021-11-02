using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupFileDownloadInfo = Mirai.CSharp.Models.IGroupFileDownloadInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface IGroupFileDownloadInfo : ISharedGroupFileDownloadInfo
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("sha1")]
        abstract string ISharedGroupFileDownloadInfo.Sha1 { get; }

        [JsonPropertyName("md5")]
        abstract string ISharedGroupFileDownloadInfo.Md5 { get; }

        [JsonPropertyName("url")]
        abstract string? ISharedGroupFileDownloadInfo.Url { get; }

        [JsonPropertyName("downloadTimes")]
        abstract int ISharedGroupFileDownloadInfo.DownloadedTimes { get; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("uploadTime")]
        abstract DateTime ISharedGroupFileDownloadInfo.CreateTime { get; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("lastModifyTime")]
        abstract DateTime ISharedGroupFileDownloadInfo.ModifyTime { get; }
#else
        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.Sha1"/>
        [JsonPropertyName("sha1")]
        new string Sha1 { get; }

        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.Md5"/>
        [JsonPropertyName("md5")]
        new string Md5 { get; }

        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.Url"/>
        [JsonPropertyName("url")]
        new string? Url { get; }
        
        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.DownloadedTimes"/>
        [JsonPropertyName("downloadTimes")]
        new int DownloadedTimes { get; }
        
        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.CreateTime"/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("uploadTime")]
        new DateTime CreateTime { get; }
        
        /// <inheritdoc cref="ISharedGroupFileDownloadInfo.ModifyTime"/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("lastModifyTime")]
        new DateTime ModifyTime { get; }
#endif
    }

    public class GroupFileDownloadInfo : IGroupFileDownloadInfo
    {
        /// <inheritdoc/>
        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("md5")]
        public string Md5 { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("downloadTimes")]
        public int DownloadedTimes { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("uploadTime")]
        public DateTime CreateTime { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("lastModifyTime")]
        public DateTime ModifyTime { get; set; }

        public GroupFileDownloadInfo()
        {

        }

        public GroupFileDownloadInfo(string sha1, string md5, string? url, int downloadedTimes, DateTime createTime, DateTime modifyTime)
        {
            Sha1 = sha1;
            Md5 = md5;
            Url = url;
            DownloadedTimes = downloadedTimes;
            CreateTime = createTime;
            ModifyTime = modifyTime;
        }
    }
}
