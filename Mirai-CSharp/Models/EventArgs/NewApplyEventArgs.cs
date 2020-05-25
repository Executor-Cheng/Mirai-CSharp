using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供用于处理相关申请所需信息的接口
    /// </summary>
    public interface IApplyResponseArgs
    {
        [JsonPropertyName("eventId")]
        long EventId { get; }

        [JsonPropertyName("fromId")]
        long FromQQ { get; }

        [JsonPropertyName("groupId")]
        long FromGroup { get; }
    }

    /// <summary>
    /// 提供通用申请相关信息的接口。继承自 <see cref="IApplyResponseArgs"/>
    /// </summary>
    public interface INewApplyEventArgs : IApplyResponseArgs
    {
        [JsonPropertyName("nick")]
        string NickName { get; }
    }

    public abstract class NewApplyEventArgs : INewApplyEventArgs
    {
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }

        [JsonPropertyName("fromId")]
        public long FromGroup { get; set; }

        [JsonPropertyName("groupId")]
        public long FromQQ { get; set; }

        [JsonPropertyName("nick")]
        public string NickName { get; set; }

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
