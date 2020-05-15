using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IApplyResponseArgs
    {
        [JsonPropertyName("eventId")]
        long EventId { get; }

        [JsonPropertyName("fromId")]
        long FromQQ { get; }

        [JsonPropertyName("groupId")]
        long FromGroup { get; }
    }

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
