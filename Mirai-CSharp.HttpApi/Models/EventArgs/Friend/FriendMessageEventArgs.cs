using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedFriendInfo = Mirai.CSharp.Models.IFriendInfo;
using ISharedFriendMessageEventArgs = Mirai.CSharp.Models.EventArgs.IFriendMessageEventArgs;
using ISharedJsonFriendMessageEventArgs = Mirai.CSharp.Models.EventArgs.IFriendMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友消息的相关信息接口。继承自 <see cref="ISharedJsonFriendMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("FriendMessage")]
    public interface IFriendMessageEventArgs : ISharedJsonFriendMessageEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedFriendMessageEventArgs.Sender"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        [JsonPropertyName("sender")]
        new IFriendInfo Sender { get; }

#if !NETSTANDARD2_0
        ISharedFriendInfo ISharedFriendMessageEventArgs.Sender => Sender;
#endif
    }

    public class FriendMessageEventArgs : CommonMessageEventArgs, IFriendMessageEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        [JsonPropertyName("sender")]
        public IFriendInfo Sender { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageEventArgs(IChatMessage[] chain, IFriendInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"{Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<ChatMessage>)Chain)}";

#if NETSTANDARD2_0
        ISharedFriendInfo ISharedFriendMessageEventArgs.Sender => Sender;
#endif
    }
}
