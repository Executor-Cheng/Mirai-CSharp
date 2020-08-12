using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群信息的接口
    /// </summary>
    public interface IGroupConfig
    {
        /// <summary>
        /// 群名
        /// </summary>
        [JsonPropertyName("name")]
        string Name { get; }
        /// <summary>
        /// 群公告
        /// </summary>
        [JsonPropertyName("announcement")]
        string Announcement { get; }
        /// <summary>
        /// 是否允许坦白说
        /// </summary>
        [JsonPropertyName("confessTalk")]
        bool? ConfessTalk { get; }
        /// <summary>
        /// 是否允许群成员邀请新用户
        /// </summary>
        [JsonPropertyName("allowMemberInvite")]
        bool? MemberInvite { get; }
        /// <summary>
        /// 是否自动通过入群申请
        /// </summary>
        [JsonPropertyName("autoApprove")]
        bool? AutoApprove { get; }
        /// <summary>
        /// 是否允许匿名聊天
        /// </summary>
        [JsonPropertyName("anonymousChat")]
        bool? AnonymousChat { get; }
    }

    public class GroupConfig : IGroupConfig
    {
        /// <summary>
        /// 群名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 群公告
        /// </summary>
        [JsonPropertyName("announcement")]
        public string Announcement { get; set; } = null!;
        /// <summary>
        /// 是否允许坦白说
        /// </summary>
        [JsonPropertyName("confessTalk")]
        public bool? ConfessTalk { get; set; }
        /// <summary>
        /// 是否允许群成员邀请新用户
        /// </summary>
        [JsonPropertyName("allowMemberInvite")]
        public bool? MemberInvite { get; set; }
        /// <summary>
        /// 是否自动通过入群申请
        /// </summary>
        [JsonPropertyName("autoApprove")]
        public bool? AutoApprove { get; set; }
        /// <summary>
        /// 是否允许匿名聊天
        /// </summary>
        [JsonPropertyName("anonymousChat")]
        public bool? AnonymousChat { get; set; }

        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public GroupConfig()
        {

        }

        public GroupConfig(string name, string announcement, bool? confessTalk, bool? memberInvite, bool? autoApprove, bool? anonymousChat)
        {
            Name = name;
            Announcement = announcement;
            ConfessTalk = confessTalk;
            MemberInvite = memberInvite;
            AutoApprove = autoApprove;
            AnonymousChat = anonymousChat;
        }
    }
}
