using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IFriendInfo : IBaseInfo
    {
        /// <summary>
        /// 好友昵称
        /// </summary>
        [JsonPropertyName("nickname")]
        abstract string IBaseInfo.Name { get; }
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
        public string Remark { get; set; }

        public FriendInfo()
        {

        }

        public FriendInfo(long id, string name, string remark) : base(id, name)
        {
            Remark = remark;
        }
    }
}
