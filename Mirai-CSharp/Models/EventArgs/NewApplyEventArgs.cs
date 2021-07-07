using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供通用申请相关信息的接口。继承自 <see cref="IApplyResponseArgs"/>
    /// </summary>
    public interface INewApplyEventArgs : IApplyResponseArgs
    {
        /// <summary>
        /// 申请人的昵称或群名片
        /// </summary>
        [JsonPropertyName("nick")]
        string NickName { get; }

        /// <summary>
        /// 申请消息
        /// </summary>
        [JsonPropertyName("message")]
        string Message { get; }
    }

    /// <summary>
    /// 通用申请相关信息的抽象基类
    /// </summary>
    public abstract class NewApplyEventArgs : ApplyResponseArgs, INewApplyEventArgs
    {
        [JsonPropertyName("nick")]
        public string NickName { get; set; } = null!;

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        protected NewApplyEventArgs() { }

        protected NewApplyEventArgs(long eventId, long fromGroup, long fromQQ, string nickName)
        {
            EventId = eventId;
            FromGroup = fromGroup;
            FromQQ = fromQQ;
            NickName = nickName;
        }
    }
}
