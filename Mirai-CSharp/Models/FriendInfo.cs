using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供好友信息的接口。继承自 <see cref="IBaseInfo"/>
    /// </summary>
    public interface IFriendInfo : IBaseInfo
    {
#if NETSTANDARD2_0
        /// <summary>
        /// 好友昵称
        /// </summary>
        [JsonPropertyName("nickname")]
        new string Name { get; }
#else
        /// <summary>
        /// 好友昵称
        /// </summary>
        [JsonPropertyName("nickname")]
        abstract string IBaseInfo.Name { get; }
#endif
        /// <summary>
        /// 好友备注
        /// </summary>
        [JsonPropertyName("remark")]
        string Remark { get; }
    }

    public class FriendInfo : BaseInfo, IFriendInfo
    {
        /// <summary>
        /// 好友昵称
        /// </summary>
        [JsonPropertyName("nickname")]
        public override string Name { get => base.Name; set => base.Name = value; }
        /// <summary>
        /// 好友备注
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = null!;

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
        [JsonPropertyName("nickname")]
        string IFriendInfo.Name => base.Name;
#endif
    }
}
