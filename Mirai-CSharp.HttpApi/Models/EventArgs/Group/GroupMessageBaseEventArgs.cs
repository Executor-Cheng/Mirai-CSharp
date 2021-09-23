using System.Collections.Generic;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedGroupMessageBaseEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMessageBaseEventArgs;
using ISharedJsonElementGroupMessageBaseEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMessageBaseEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群消息和临时消息的相关信息基接口。继承自 <see cref="ISharedJsonElementGroupMessageBaseEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IGroupMessageBaseEventArgs : ISharedJsonElementGroupMessageBaseEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedGroupMessageBaseEventArgs.Sender"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        new IGroupMemberInfo Sender { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        ISharedGroupMemberInfo ISharedGroupMessageBaseEventArgs.Sender => Sender;
#endif
    }

    public abstract class GroupMessageBaseEventArgs : CommonMessageEventArgs, IGroupMessageBaseEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        public IGroupMemberInfo Sender { get; set; } = null!;

        protected GroupMessageBaseEventArgs() { }

        protected GroupMessageBaseEventArgs(IChatMessage[] chain, IGroupMemberInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<ChatMessage>)Chain)}";

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        ISharedGroupMemberInfo ISharedGroupMessageBaseEventArgs.Sender => Sender;
#endif
    }
}
