using System;
using System.Text.Json.Serialization;
using ISharedFriendInfo = Mirai.CSharp.Models.IFriendInfo;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <summary>
    /// 提供好友信息的接口。继承自 <see cref="ISharedFriendInfo"/>
    /// </summary>
    public interface IFriendInfo : ISharedFriendInfo
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedFriendInfo.Remark"/>
        [JsonPropertyName("remark")]
        new string Remark { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("remark")]
        abstract string ISharedFriendInfo.Remark { get; }
#endif
    }

    public class FriendInfo : BaseInfo, IFriendInfo
    {
        /// <inheritdoc/>
        [JsonPropertyName("nickname")]
        public override string Name { get => base.Name; set => base.Name = value; }
        /// <inheritdoc/>
        [JsonPropertyName("remark")]
        public virtual string Remark { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendInfo()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendInfo(long id, string name, string remark) : base(id, name)
        {
            Remark = remark;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("remark")]
        string ISharedFriendInfo.Remark => Remark;
#endif
    }
}
