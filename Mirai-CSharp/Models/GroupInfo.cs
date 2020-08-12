using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供基本信息(Id和名称)的接口
    /// </summary>
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
        public virtual string Name { get; set; } = null!;

        protected BaseInfo()
        {

        }

        protected BaseInfo(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    /// <summary>
    /// 提供群内权限和基本信息的接口。继承自 <see cref="IBaseInfo"/>
    /// </summary>
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

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupInfo() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupInfo(long id, string name, GroupPermission permission)
        {
            Id = id;
            Name = name;
            Permission = permission;
        }
    }
}
