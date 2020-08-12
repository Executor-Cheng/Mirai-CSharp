using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群名片和专属头衔的接口
    /// </summary>
    public interface IGroupMemberCardInfo
    {
        /// <summary>
        /// 群名片
        /// </summary>
        [JsonPropertyName("name")]
        string Name { get; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        [JsonPropertyName("specialTitle")]
        string SpecialTitle { get; }
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
            Name = name;
            SpecialTitle = specialTitle;
        }
    }
}
