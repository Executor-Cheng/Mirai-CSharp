using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
using ISharedForwardMessageNode = Mirai.CSharp.Models.ChatMessages.IForwardMessageNode;
using ISharedForwardMessageReference = Mirai.CSharp.Models.ChatMessages.IForwardMessageNodeReference;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    public interface IForwardMessageNode : ISharedForwardMessageNode
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("sourceId")]
        abstract int? ISharedForwardMessageNode.Id { get; }

        [JsonPropertyName("senderName")]
        abstract string? ISharedForwardMessageNode.Name { get; }

        [JsonPropertyName("senderId")]
        abstract long? ISharedForwardMessageNode.QQNumber { get; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        abstract DateTime? ISharedForwardMessageNode.Time { get; }

        [JsonPropertyName("messageChain")]
        ISharedChatMessage[]? ISharedForwardMessageNode.Chain => Chain;
        
        [JsonPropertyName("messageRef")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageReference, ForwardMessageNodeReference>))]
        ISharedForwardMessageReference? ISharedForwardMessageNode.Reference => Reference;
#else
        [JsonPropertyName("sourceId")]
        new int? Id { get; }
        
        [JsonPropertyName("senderName")]
        new string? Name { get; }
        
        [JsonPropertyName("senderId")]
        new long? QQNumber { get; }
        
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        new DateTime? Time { get; }
#endif

        [JsonPropertyName("messageChain")]
        new IChatMessage[]? Chain { get; }

        [JsonPropertyName("messageRef")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageNodeReference, ForwardMessageNodeReference>))]
        new IForwardMessageNodeReference? Reference { get; }
    }

    public class ForwardMessageNode : IForwardMessageNode
    {
        [JsonPropertyName("sourceId")]
        public int? Id { get; set; }

        [JsonPropertyName("senderName")]
        public string? Name { get; set; }

        [JsonPropertyName("senderId")]
        public long? QQNumber { get; set; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public DateTime? Time { get; set; }

        [JsonPropertyName("messageChain")]
        public IChatMessage[]? Chain { get; set; }

        [JsonPropertyName("messageRef")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IForwardMessageNodeReference, ForwardMessageNodeReference>))]
        public IForwardMessageNodeReference? Reference { get; set; }

        public ForwardMessageNode()
        {

        }

        public ForwardMessageNode(int id)
        {
            Id = id;
        }

        public ForwardMessageNode(string name, long qqNumber, DateTime time, IChatMessage[] chain)
        {
            Name = name;
            QQNumber = qqNumber;
            Time = time;
            Chain = chain;
        }

        public ForwardMessageNode(IForwardMessageNodeReference reference)
        {
            Reference = reference;
        }

        public override string ToString()
        {
            return $"[mirai:forward:{Chain?.Length ?? 0} nodes]";
        }

#if NETSTANDARD2_0
        [JsonPropertyName("messageChain")]
        ISharedChatMessage[]? ISharedForwardMessageNode.Chain => Chain;
        
        [JsonPropertyName("messageRef")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedForwardMessageReference, ForwardMessageNodeReference>))]
        ISharedForwardMessageReference? ISharedForwardMessageNode.Reference => Reference;
#endif
    }
}
