using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedTempSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.ITempSyncMessageEventArgs;
using ISharedJsonElementTempSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.ITempSyncMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供临时同步消息的相关信息接口。继承自 <see cref="ISharedJsonElementTempSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("TempSyncMessage")]
    public interface ITempSyncMessageEventArgs : ISharedJsonElementTempSyncMessageEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedTempSyncMessageEventArgs.Subject"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("subject")]
        new IGroupMemberInfo Subject { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("subject")]
        ISharedGroupMemberInfo ISharedTempSyncMessageEventArgs.Subject => Subject;
#endif
    }

    public class TempSyncMessageEventArgs : CommonMessageEventArgs, ITempSyncMessageEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("subject")]
        public IGroupMemberInfo Subject { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public TempSyncMessageEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public TempSyncMessageEventArgs(IChatMessage[] chain, IGroupMemberInfo subject) : base(chain)
        {
            Subject = subject;
        }

        public override string ToString()
            => $"{Subject.Name}(Temp {Subject.Id})[SYNC] <- {string.Join("", (IEnumerable<ChatMessage>)Chain)}";

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("subject")]
        ISharedGroupMemberInfo ISharedTempSyncMessageEventArgs.Subject => Subject;
#endif
    }
}
