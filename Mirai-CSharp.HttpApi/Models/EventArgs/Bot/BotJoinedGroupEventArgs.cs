using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedBotJoinedGroupEventArgs = Mirai.CSharp.Models.EventArgs.IBotJoinedGroupEventArgs<System.Text.Json.JsonElement>;
#if NETSTANDARD2_0
using ISharedInviterEventArgs = Mirai.CSharp.Models.EventArgs.IInviterEventArgs;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
#endif

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="ISharedBotJoinedGroupEventArgs"/> 和 <see cref="IGroupEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("BotJoinGroupEvent")]
    public interface IBotJoinedGroupEventArgs : ISharedBotJoinedGroupEventArgs, IGroupEventArgs, IInviterEventArgs
    {

    }

    public class BotJoinedGroupEventArgs : GroupEventArgs, IBotJoinedGroupEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("invitor")]
        public IGroupMemberInfo? Inviter { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public BotJoinedGroupEventArgs(GroupInfo group) : base(group)
        {

        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, ISharedGroupMemberInfo>))]
        [JsonPropertyName("invitor")]
        ISharedGroupMemberInfo? ISharedInviterEventArgs.Inviter => Inviter;
#endif
    }
}
