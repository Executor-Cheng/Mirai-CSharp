using System.Text.Json.Serialization;
using ISharedForwardMessageReference = Mirai.CSharp.Models.ChatMessages.IForwardMessageNodeReference;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    public interface IForwardMessageNodeReference : ISharedForwardMessageReference
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("messageId")]
        abstract int ISharedForwardMessageReference.MessageId { get; }

        [JsonPropertyName("target")]
        abstract long ISharedForwardMessageReference.Target { get; }
#else
        [JsonPropertyName("messageId")]
        new int MessageId { get; }
        
        [JsonPropertyName("target")]
        new long Target { get; }
#endif
    }

    public class ForwardMessageNodeReference : IForwardMessageNodeReference
    {
        [JsonPropertyName("messageId")]
        public int MessageId { get; set; }

        [JsonPropertyName("target")]
        public long Target { get; set; }

        public ForwardMessageNodeReference()
        {

        }

        public ForwardMessageNodeReference(int messageId, long target)
        {
            MessageId = messageId;
            Target = target;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("messageId")]
        int ISharedForwardMessageReference.MessageId => MessageId;

        [JsonPropertyName("target")]
        long ISharedForwardMessageReference.Target => Target;
#endif
    }
}
