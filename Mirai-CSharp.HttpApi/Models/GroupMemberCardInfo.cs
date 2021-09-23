using System;
using System.Text.Json.Serialization;
using ISharedGroupMemberCardInfo = Mirai.CSharp.Models.IGroupMemberCardInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface IGroupMemberCardInfo : ISharedGroupMemberCardInfo
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedGroupMemberCardInfo.Name"/>
        [JsonPropertyName("name")]
        new string Name { get; }
        /// <inheritdoc cref="ISharedGroupMemberCardInfo.SpecialTitle"/>
        [JsonPropertyName("specialTitle")]
        new string SpecialTitle { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        abstract string ISharedGroupMemberCardInfo.Name { get; }
        /// <inheritdoc/>
        [JsonPropertyName("specialTitle")]
        abstract string ISharedGroupMemberCardInfo.SpecialTitle { get; }
#endif
    }

    public class GroupMemberCardInfo : IGroupMemberCardInfo
    {
        /// <summary>
        /// 群名片
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 专属头衔
        /// </summary>
        [JsonPropertyName("specialTitle")]
        public string SpecialTitle { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardInfo()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberCardInfo(string name, string specialTitle)
        {
            
        }

#if NETSTANDARD2_0
        [JsonPropertyName("name")]
        string ISharedGroupMemberCardInfo.Name => Name;

        [JsonPropertyName("specialTitle")]
        string ISharedGroupMemberCardInfo.SpecialTitle => SpecialTitle;
#endif
    }
}
