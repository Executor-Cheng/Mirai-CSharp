using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using Mirai.CSharp.Models;
using ISharedBaseInfo = Mirai.CSharp.Models.IBaseInfo;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
#if NETCOREAPP3_0 || NETCOREAPP3_1
using NullableUnixTimeStampJsonConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.NullableJsonConverter<System.DateTime, Mirai.CSharp.HttpApi.Utility.JsonConverters.UnixTimeStampJsonConverter>;
using NullableTimeSpanSecondsConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.NullableJsonConverter<System.TimeSpan, Mirai.CSharp.HttpApi.Utility.JsonConverters.TimeSpanSecondsConverter>;
#else
using NullableUnixTimeStampJsonConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.UnixTimeStampJsonConverter;
using NullableTimeSpanSecondsConverter = Mirai.CSharp.HttpApi.Utility.JsonConverters.TimeSpanSecondsConverter;
#endif

namespace Mirai.CSharp.HttpApi.Models
{
    /// <summary>
    /// 提供群成员信息的接口。继承自 <see cref="IGroupInfo"/> 和 <see cref="Mirai.CSharp.Models.IGroupMemberInfo"/>
    /// </summary>
    public interface IGroupMemberInfo : IGroupInfo, ISharedGroupMemberInfo
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedBaseInfo.Name"/>
        [JsonPropertyName("memberName")]
        new string Name { get; }
        
        /// <inheritdoc cref="ISharedGroupMemberInfo.SpecialTitle"/>
        [JsonPropertyName("specialTitle")]
        new string? SpecialTitle { get; set; }
        
        /// <inheritdoc cref="ISharedGroupMemberInfo.JoinTime"/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("joinTimestamp")]
        new DateTime? JoinTime { get; set; }
        
        /// <inheritdoc cref="ISharedGroupMemberInfo.LastSpeakTime"/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("lastSpeakTimestamp")]
        new DateTime? LastSpeakTime { get; set; }
        
        /// <inheritdoc cref="ISharedGroupMemberInfo.MuteTimeRemaining"/>
        [JsonConverter(typeof(NullableTimeSpanSecondsConverter))]
        [JsonPropertyName("muteTimeRemaining")]
        new TimeSpan? MuteTimeRemaining { get; set; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("memberName")]
        abstract string ISharedBaseInfo.Name { get; }

        [JsonPropertyName("specialTitle")]
        abstract string? ISharedGroupMemberInfo.SpecialTitle { get; set; }

        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("joinTimestamp")]
        abstract DateTime? ISharedGroupMemberInfo.JoinTime { get; set; }

        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("lastSpeakTimestamp")]
        abstract DateTime? ISharedGroupMemberInfo.LastSpeakTime { get; set; }

        [JsonConverter(typeof(NullableTimeSpanSecondsConverter))]
        [JsonPropertyName("muteTimeRemaining")]
        abstract TimeSpan? ISharedGroupMemberInfo.MuteTimeRemaining { get; set; }
#endif

        /// <inheritdoc cref="ISharedGroupMemberInfo.Group"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        new IGroupInfo Group { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupMemberInfo.Group => Group;
#endif
    }

    public class GroupMemberInfo : GroupInfo, IGroupMemberInfo
    {
        /// <inheritdoc/>
        [JsonPropertyName("memberName")]
        public override string Name { get; set; } = null!;

        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("specialTitle")]
        public string? SpecialTitle { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("joinTimestamp")]
        public DateTime? JoinTime { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(NullableUnixTimeStampJsonConverter))]
        [JsonPropertyName("lastSpeakTimestamp")]
        public DateTime? LastSpeakTime { get; set; }

        /// <inheritdoc/>
        [JsonConverter(typeof(NullableTimeSpanSecondsConverter))]
        [JsonPropertyName("muteTimeRemaining")]
        public TimeSpan? MuteTimeRemaining { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInfo() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInfo(IGroupInfo group, long id, string name, GroupPermission permission) : base(id, name, permission)
        {
            Group = group;
        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInfo(IGroupInfo group, long id, string name, GroupPermission permission, string? specialTitle, DateTime? joinTime, DateTime? lastSpeakTime, TimeSpan? muteTimeRemaining) : this(group, id, name, permission)
        {
            SpecialTitle = specialTitle;
            JoinTime = joinTime;
            LastSpeakTime = lastSpeakTime;
            MuteTimeRemaining = muteTimeRemaining;
        }
#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupMemberInfo.Group => Group;
#endif
    }
}
