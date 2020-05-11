using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
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
        public string Name { get; set; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        [JsonPropertyName("specialTitle")]
        public string SpecialTitle { get; set; }

        public GroupMemberCardInfo()
        {

        }

        public GroupMemberCardInfo(string name, string specialTitle)
        {
            Name = name;
            SpecialTitle = specialTitle;
        }
    }
}
