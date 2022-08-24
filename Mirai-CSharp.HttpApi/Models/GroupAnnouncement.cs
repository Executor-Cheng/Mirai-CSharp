using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupAnnouncement = Mirai.CSharp.Models.IGroupAnnouncement;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
#if NETCOREAPP3_0 || NETCOREAPP3_1
using NullableUnixTimeStampJsonConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.NullableJsonConverter<System.DateTime, Mirai.CSharp.HttpApi.Utility.JsonConverters.UnixTimeStampJsonConverter>;
#else
using NullableUnixTimeStampJsonConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.UnixTimeStampJsonConverter;
#endif

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models
{
    public interface IGroupAnnouncement : ISharedGroupAnnouncement
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedGroupAnnouncement.Id"/>
        [JsonPropertyName("fid")]
        new string Id { get; }
        /// <inheritdoc cref="ISharedGroupAnnouncement.Content"/>
        [JsonPropertyName("content")]
        new string Content { get; }
        /// <inheritdoc cref="ISharedGroupAnnouncement.Sender"/>
        [JsonPropertyName("senderId")]
        new long Sender { get; }
        /// <inheritdoc cref="ISharedGroupAnnouncement.AllMemberConfirmed"/>
        [JsonPropertyName("allConfirmed")]
        new bool AllMemberConfirmed { get; }
        /// <inheritdoc cref="ISharedGroupAnnouncement.ConfirmedMembersCount"/>
        [JsonPropertyName("confirmedMembersCount")]
        new int ConfirmedMembersCount { get; }
        /// <inheritdoc cref="ISharedGroupAnnouncement.CreateTime"/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("publicationTime")]
        new DateTime CreateTime { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("fid")]
        abstract string ISharedGroupAnnouncement.Id { get; }
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        abstract string ISharedGroupAnnouncement.Content { get; }
        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        abstract long ISharedGroupAnnouncement.Sender { get; }
        /// <inheritdoc/>
        [JsonPropertyName("allConfirmed")]
        abstract bool ISharedGroupAnnouncement.AllMemberConfirmed { get; }
        /// <inheritdoc/>
        [JsonPropertyName("confirmedMembersCount")]
        abstract int ISharedGroupAnnouncement.ConfirmedMembersCount { get; }
        /// <inheritdoc/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("publicationTime")]
        abstract DateTime ISharedGroupAnnouncement.CreateTime { get; }
#endif
        /// <inheritdoc cref="ISharedGroupAnnouncement.Group"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        new IGroupInfo Group { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupAnnouncement.Group => Group;
#endif
    }

    public class GroupAnnouncement : IGroupAnnouncement
    {
        /// <inheritdoc/>
        [JsonPropertyName("fid")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public IGroupInfo Group { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        public long Sender { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("allConfirmed")]
        public bool AllMemberConfirmed { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("confirmedMembersCount")]
        public int ConfirmedMembersCount { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("publicationTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 初始化 <see cref="GroupAnnouncement"/> 类的新实例
        /// </summary>
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupAnnouncement() { }

        /// <summary>
        /// 初始化 <see cref="GroupAnnouncement"/> 类的新实例
        /// </summary>
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupAnnouncement(string id, IGroupInfo group, string content, long sender, bool allMemberConfirmed, int confirmedMembersCount, DateTime createTime)
        {
            Id = id;
            Group = group;
            Content = content;
            Sender = sender;
            AllMemberConfirmed = allMemberConfirmed;
            ConfirmedMembersCount = confirmedMembersCount;
            CreateTime = createTime;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("fid")]
        string ISharedGroupAnnouncement.Id => Id;
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        string ISharedGroupAnnouncement.Content => Content;
        /// <inheritdoc/>
        [JsonPropertyName("senderId")]
        long ISharedGroupAnnouncement.Sender => Sender;
        /// <inheritdoc/>
        [JsonPropertyName("allConfirmed")]
        bool ISharedGroupAnnouncement.AllMemberConfirmed => AllMemberConfirmed;
        /// <inheritdoc/>
        [JsonPropertyName("confirmedMembersCount")]
        int ISharedGroupAnnouncement.ConfirmedMembersCount => ConfirmedMembersCount;
        /// <inheritdoc/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("publicationTime")]
        DateTime ISharedGroupAnnouncement.CreateTime => CreateTime;
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupAnnouncement.Group => Group;
#endif
    }
}
