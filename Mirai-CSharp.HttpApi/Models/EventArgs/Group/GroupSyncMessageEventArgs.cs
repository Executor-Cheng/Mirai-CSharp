using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.IGroupSyncMessageEventArgs;
using ISharedJsonElementGroupSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.IGroupSyncMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群同步消息相关信息的接口。继承自 <see cref="ISharedJsonElementGroupSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupSyncMessage")]
    public interface IGroupSyncMessageEventArgs : ISharedJsonElementGroupSyncMessageEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedGroupSyncMessageEventArgs.Subject"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupInfo, GroupInfo>))]
        [JsonPropertyName("subject")]
        new IGroupInfo Subject { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("subject")]
        ISharedGroupInfo ISharedGroupSyncMessageEventArgs.Subject => Subject;
#endif
    }

    public class GroupSyncMessageEventArgs : CommonMessageEventArgs, IGroupSyncMessageEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupInfo, GroupInfo>))]
        [JsonPropertyName("subject")]
        public IGroupInfo Subject { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupSyncMessageEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupSyncMessageEventArgs(IChatMessage[] chain, IGroupInfo subject) : base(chain)
        {
            Subject = subject;
        }

        public override string ToString()
            => $"[{Subject.Name}({Subject.Id})][SYNC] <- {string.Join("", (IEnumerable<ChatMessage>)Chain)}";

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupInfo, GroupInfo>))]
        [JsonPropertyName("subject")]
        ISharedGroupInfo ISharedGroupSyncMessageEventArgs.Subject => Subject;
#endif
    }
}
