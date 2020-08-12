using Mirai_CSharp.Utility.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群成员信息的接口。继承自 <see cref="IGroupInfo"/>
    /// </summary>
    public interface IGroupMemberInfo : IGroupInfo
    {
#if NETSTANDARD2_0
        /// <summary>
        /// 成员昵称
        /// </summary>
        [JsonPropertyName("memberName")]
        new string Name { get; }
#else
        /// <summary>
        /// 成员昵称
        /// </summary>
        [JsonPropertyName("memberName")]
        abstract string IBaseInfo.Name { get; }
#endif

        /// <summary>
        /// 机器人所在群的信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        IGroupInfo Group { get; }
    }

    public class GroupMemberInfo : GroupInfo, IGroupMemberInfo
    {
        /// <summary>
        /// 成员昵称
        /// </summary>
        [JsonPropertyName("memberName")]
        public override string Name { get => base.Name; set => base.Name = value; }
        /// <summary>
        /// 机器人所在群的信息
        /// </summary>
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupInfo, IGroupInfo>))]
        [JsonPropertyName("group")]
        public IGroupInfo Group { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInfo() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMemberInfo(IGroupInfo args, long id, string name, GroupPermission permission) : base(id, name, permission)
        {
            Group = args;
        }
#if NETSTANDARD2_0
        [JsonPropertyName("memberName")]
        string IBaseInfo.Name => base.Name;
#endif
    }
}
