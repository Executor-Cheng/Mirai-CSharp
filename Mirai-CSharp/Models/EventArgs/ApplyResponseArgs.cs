using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供用于处理相关申请所需信息的接口
    /// </summary>
    public interface IApplyResponseArgs : IEventArgsBase
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
    /// 用于处理相关申请所需信息的抽象基类
    /// </summary>
    public abstract class ApplyResponseArgs : EventArgsBase, IApplyResponseArgs
    {
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }

        [JsonPropertyName("fromId")]
        public long FromQQ { get; set; }

        [JsonPropertyName("groupId")]
        public long FromGroup { get; set; }
    }
}
