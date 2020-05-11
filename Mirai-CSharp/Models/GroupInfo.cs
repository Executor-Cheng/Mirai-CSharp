using Mirai_CSharp.Utility.JsonConverters;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IBaseInfo
    {
        /// <summary>
        /// QQ号/群号
        /// </summary>
        [JsonPropertyName("id")]
        long Id { get; }
        /// <summary>
        /// 昵称/群名
        /// </summary>
        [JsonPropertyName("name")]
        string Name { get; }
    }

    public abstract class BaseInfo : IBaseInfo
    {
        /// <summary>
        /// QQ号/群号
        /// </summary>
        [JsonPropertyName("id")]
        public virtual long Id { get; set; }
        /// <summary>
        /// 昵称/群名
        /// </summary>
        [JsonPropertyName("name")]
        public virtual string Name { get; set; }

        protected BaseInfo()
        {

        }

        protected BaseInfo(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public interface IGroupInfo : IBaseInfo
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("permission")]
        GroupPermission Permission { get; }
    }

    public class GroupInfo : BaseInfo, IGroupInfo
    {
        /// <summary>
        /// Bot在群内的权限
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("permission")]
        public virtual GroupPermission Permission { get; set; }

        public GroupInfo() { }

        public GroupInfo(long id, string name, GroupPermission permission)
        {
            Id = id;
            Name = name;
            Permission = permission;
        }
    }
}
