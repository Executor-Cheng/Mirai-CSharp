using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供用于处理相关申请所需信息的接口
    /// </summary>
    public interface IApplyResponseArgs
    {
        /// <summary>
        /// 事件Id, 供 mirai-api-http 使用
        /// </summary>
        [JsonPropertyName("eventId")]
        long EventId { get; }

        /// <summary>
        /// 申请人QQ号
        /// </summary>
        [JsonPropertyName("fromId")]
        long FromQQ { get; }

        /// <summary>
        /// 申请来源群号
        /// </summary>
        /// <remarks>
        /// 在好友添加事件中, 如果申请人通过某个群添加好友, 该项为该群群号, 否则为0
        /// </remarks>
        [JsonPropertyName("groupId")]
        long FromGroup { get; }
    }

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

    public abstract class NewApplyEventArgs : INewApplyEventArgs
    {
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }

        [JsonPropertyName("fromId")]
        public long FromQQ { get; set; }

        [JsonPropertyName("groupId")]
        public long FromGroup { get; set; }

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
