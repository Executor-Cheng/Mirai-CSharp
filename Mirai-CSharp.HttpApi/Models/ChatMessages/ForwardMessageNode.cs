using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
using ISharedForwardMessageNode = Mirai.CSharp.Models.ChatMessages.IForwardMessageNode;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    public interface IForwardMessageNode : ISharedForwardMessageNode
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("sourceId")]
        abstract int ISharedForwardMessageNode.Id { get; }

        [JsonPropertyName("senderName")]
        abstract string ISharedForwardMessageNode.Name { get; }

        [JsonPropertyName("senderId")]
        abstract long ISharedForwardMessageNode.QQNumber { get; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        abstract DateTime ISharedForwardMessageNode.Time { get; }

        [JsonPropertyName("messageChain")]
        ISharedChatMessage[] ISharedForwardMessageNode.Chain => Chain;
#else
        [JsonPropertyName("sourceId")]
        new int Id { get; }
        
        [JsonPropertyName("senderName")]
        new string Name { get; }
        
        [JsonPropertyName("senderId")]
        new long QQNumber { get; }
        
        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        new DateTime Time { get; }
#endif

        [JsonPropertyName("messageChain")]
        new IChatMessage[] Chain { get; }
    }

    public class ForwardMessageNode : IForwardMessageNode
    {
        [JsonPropertyName("sourceId")]
        public int Id { get; set; }

        [JsonPropertyName("senderName")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("senderId")]
        public long QQNumber { get; set; }

        [JsonConverter(typeof(UnixTimeStampJsonConverter))]
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("messageChain")]
        public IChatMessage[] Chain { get; set; } = null!;

        public ForwardMessageNode()
        {

        }

        public ForwardMessageNode(int id)
        {
            Id = id;
        }

        public ForwardMessageNode(string name, long qQNumber, DateTime time, IChatMessage[] chain)
        {
            Name = name;
            QQNumber = qQNumber;
            Time = time;
            Chain = chain;
        }

        public override string ToString()
        {
            return $"[mirai:forward:{Chain.Length} nodes]";
        }

#if NETSTANDARD2_0
        [JsonPropertyName("messageChain")]
        ISharedChatMessage[] ISharedForwardMessageNode.Chain => Chain;
#endif
    }
}
