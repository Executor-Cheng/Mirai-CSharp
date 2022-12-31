using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedFriendInfo = Mirai.CSharp.Models.IFriendInfo;
using ISharedFriendSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.IFriendSyncMessageEventArgs;
using ISharedJsonFriendMessageEventArgs = Mirai.CSharp.Models.EventArgs.IFriendSyncMessageEventArgs<System.Text.Json.JsonElement>;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友同步消息的相关信息接口。继承自 <see cref="ISharedJsonFriendMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("FriendSyncMessage")]
    public interface IFriendSyncMessageEventArgs : ISharedJsonFriendMessageEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedFriendSyncMessageEventArgs.Subject"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        [JsonPropertyName("subject")]
        new IFriendInfo Subject { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedFriendInfo, FriendInfo>))]
        [JsonPropertyName("subject")]
        ISharedFriendInfo ISharedFriendSyncMessageEventArgs.Subject => Subject;
#endif
    }

    public class FriendSyncMessageEventArgs : CommonMessageEventArgs, IFriendSyncMessageEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        [JsonPropertyName("subject")]
        public IFriendInfo Subject { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendSyncMessageEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendSyncMessageEventArgs(IChatMessage[] chain, IFriendInfo subject) : base(chain)
        {
            Subject = subject;
        }

        public override string ToString()
            => $"{Subject.Name}({Subject.Id})[SYNC] <- {string.Join("", (IEnumerable<ChatMessage>)Chain)}";

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedFriendInfo, FriendInfo>))]
        [JsonPropertyName("subject")]
        ISharedFriendInfo ISharedFriendSyncMessageEventArgs.Subject => Subject;
#endif
    }
}
