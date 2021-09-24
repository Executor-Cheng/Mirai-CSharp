using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.Models;
using ISharedBaseInfo = Mirai.CSharp.Models.IBaseInfo;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <inheritdoc/>
    public interface IBaseInfo : ISharedBaseInfo
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedBaseInfo.Id"/>
        [JsonPropertyName("id")]
        new long Id { get; }
        /// <inheritdoc cref="ISharedBaseInfo.Name"/>
        [JsonPropertyName("name")]
        new string Name { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        abstract long ISharedBaseInfo.Id { get; }
        /// <inheritdoc/>
        [JsonPropertyName("name")]
        abstract string ISharedBaseInfo.Name { get; }
#endif
    }

    public abstract class BaseInfo : IBaseInfo
    {
        /// <inheritdoc/>
        [JsonPropertyName("id")]
        public virtual long Id { get; set; }
        /// <inheritdoc/>
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

#if NETSTANDARD2_0
        [JsonPropertyName("id")]
        long ISharedBaseInfo.Id => Id;

        [JsonPropertyName("name")]
        string ISharedBaseInfo.Name => Name;
#endif
    }
    
    /// <summary>
    /// 提供群内权限和基本信息的接口。继承自 <see cref="IBaseInfo"/>
    /// </summary>
    public interface IGroupInfo : ISharedGroupInfo, IBaseInfo
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedGroupInfo.Permission"/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("permission")]
        new GroupPermission Permission { get; }
#else
        /// <inheritdoc/>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("permission")]
        abstract GroupPermission ISharedGroupInfo.Permission { get; }
#endif
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
        public GroupInfo(long id, string name, GroupPermission permission) : base(id, name)
        {
            Permission = permission;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("permission")]
        GroupPermission ISharedGroupInfo.Permission => Permission;
#endif
    }
}
