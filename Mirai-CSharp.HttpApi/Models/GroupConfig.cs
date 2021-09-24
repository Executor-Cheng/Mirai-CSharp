using System;
using System.Text.Json.Serialization;
using ISharedGroupConfig = Mirai.CSharp.Models.IGroupConfig;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface IGroupConfig : ISharedGroupConfig
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedGroupConfig.Name"/>
        [JsonPropertyName("name")]
        new string Name { get; }
        /// <inheritdoc cref="ISharedGroupConfig.Announcement"/>
        [JsonPropertyName("announcement")]
        new string Announcement { get; }
        /// <inheritdoc cref="ISharedGroupConfig.ConfessTalk"/>
        [JsonPropertyName("confessTalk")]
        new bool? ConfessTalk { get; }
        /// <inheritdoc cref="ISharedGroupConfig.MemberInvite"/>
        [JsonPropertyName("allowMemberInvite")]
        new bool? MemberInvite { get; }
        /// <inheritdoc cref="ISharedGroupConfig.AutoApprove"/>
        [JsonPropertyName("autoApprove")]
        new bool? AutoApprove { get; }
        /// <inheritdoc cref="ISharedGroupConfig.AnonymousChat"/>
        [JsonPropertyName("anonymousChat")]
        new bool? AnonymousChat { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        abstract string ISharedGroupConfig.Name { get; }
        /// <inheritdoc/>
        [JsonPropertyName("announcement")]
        abstract string ISharedGroupConfig.Announcement { get; }
        /// <inheritdoc/>
        [JsonPropertyName("confessTalk")]
        abstract bool? ISharedGroupConfig.ConfessTalk { get; }
        /// <inheritdoc/>
        [JsonPropertyName("allowMemberInvite")]
        abstract bool? ISharedGroupConfig.MemberInvite { get; }
        /// <inheritdoc/>
        [JsonPropertyName("autoApprove")]
        abstract bool? ISharedGroupConfig.AutoApprove { get; }
        /// <inheritdoc/>
        [JsonPropertyName("anonymousChat")]
        abstract bool? ISharedGroupConfig.AnonymousChat { get; }
#endif
    }

    public class GroupConfig : IGroupConfig
    {
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        /// <inheritdoc/>
        [JsonPropertyName("announcement")]
        public string Announcement { get; set; } = null!;
        /// <inheritdoc/>
        [JsonPropertyName("confessTalk")]
        public bool? ConfessTalk { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("allowMemberInvite")]
        public bool? MemberInvite { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("autoApprove")]
        public bool? AutoApprove { get; set; }
        /// <inheritdoc/>
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

#if NETSTANDARD2_0
        [JsonPropertyName("name")]
        string ISharedGroupConfig.Name => Name;

        [JsonPropertyName("announcement")]
        string ISharedGroupConfig.Announcement => Announcement;

        [JsonPropertyName("confessTalk")]
        bool? ISharedGroupConfig.ConfessTalk => ConfessTalk;

        [JsonPropertyName("allowMemberInvite")]
        bool? ISharedGroupConfig.MemberInvite => MemberInvite;

        [JsonPropertyName("autoApprove")]
        bool? ISharedGroupConfig.AutoApprove => AutoApprove;

        [JsonPropertyName("anonymousChat")]
        bool? ISharedGroupConfig.AnonymousChat => AnonymousChat;
#endif
    }
}
