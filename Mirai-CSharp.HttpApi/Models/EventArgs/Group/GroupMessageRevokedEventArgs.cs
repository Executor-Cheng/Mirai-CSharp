using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupEventArgs = Mirai.CSharp.Models.EventArgs.IGroupEventArgs;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
using ISharedGroupMessageRevokedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMessageRevokedEventArgs<System.Text.Json.JsonElement>;
using ISharedOperatorEventArgs = Mirai.CSharp.Models.EventArgs.IOperatorEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供群内撤回消息的相关信息接口。继承自 <see cref="ISharedGroupMessageRevokedEventArgs"/>, <see cref="IGroupOperatingEventArgs"/> 和 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("GroupRecallEvent")]
    public interface IGroupMessageRevokedEventArgs : ISharedGroupMessageRevokedEventArgs, IGroupOperatingEventArgs, IMessageRevokedEventArgs
    {

    }

    public class GroupMessageRevokedEventArgs : MessageRevokedEventArgs, IGroupMessageRevokedEventArgs
    {
        /// <summary>
        /// 被撤回消息所在群信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("operator")]
        public IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageRevokedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageRevokedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, long senderId, int messageId, DateTime sentTime) : base(senderId, messageId, sentTime)
        {
            Group = group;
            Operator = @operator;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, ISharedGroupInfo>))]
        [JsonPropertyName("group")]
        ISharedGroupInfo ISharedGroupEventArgs.Group => Group;
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("member")]
        ISharedGroupMemberInfo ISharedOperatorEventArgs.Operator => Operator;
#endif
    }
}
