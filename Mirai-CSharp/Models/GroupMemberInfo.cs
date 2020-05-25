using Mirai_CSharp.Utility.JsonConverters;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群成员信息的接口。继承自 <see cref="IGroupInfo"/>
    /// </summary>
    public interface IGroupMemberInfo : IGroupInfo
    {
        /// <summary>
        /// 成员昵称
        /// </summary>
        [JsonPropertyName("memberName")]
        abstract string IBaseInfo.Name { get; }

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
        public IGroupInfo Group { get; set; }

        public GroupMemberInfo() { }

        public GroupMemberInfo(IGroupInfo args, long id, string name, GroupPermission permission) : base(id, name, permission)
        {
            Group = args;
        }
    }
}
