using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedGroupMemberJoinedEventArgs = Mirai.CSharp.Models.EventArgs.IGroupMemberJoinedEventArgs<System.Text.Json.JsonElement>;
#if NETSTANDARD2_0
using ISharedInviterEventArgs = Mirai.CSharp.Models.EventArgs.IInviterEventArgs;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
#endif

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="ISharedGroupMemberJoinedEventArgs"/> 和 <see cref="IMemberEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("MemberJoinEvent")]
    public interface IGroupMemberJoinedEventArgs : ISharedGroupMemberJoinedEventArgs, IMemberEventArgs, IInviterEventArgs
    {

    }

    public class GroupMemberJoinedEventArgs : MemberEventArgs, IGroupMemberJoinedEventArgs
    {
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("invitor")]
        public IGroupMemberInfo? Inviter { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberJoinedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberJoinedEventArgs(IGroupMemberInfo member) : base(member)
        {

        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedGroupMemberInfo, GroupMemberInfo>))]
        [JsonPropertyName("invitor")]
        ISharedGroupMemberInfo? ISharedInviterEventArgs.Inviter => Inviter;
#endif
    }
}
