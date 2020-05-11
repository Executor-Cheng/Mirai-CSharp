using Mirai_CSharp.Utility.JsonConverters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IFriendMessageEventArgs : ICommonMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<FriendInfo, IFriendInfo>))]
        [JsonPropertyName("sender")]
        IFriendInfo Sender { get; set; }
    }

    public class FriendMessageEventArgs : CommonMessageEventArgs, IFriendMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<FriendInfo, IFriendInfo>))]
        [JsonPropertyName("sender")]
        public IFriendInfo Sender { get; set; }

        public FriendMessageEventArgs() { }

        public FriendMessageEventArgs(IMessageBase[] chain, IFriendInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"{Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<MessageBase>)Chain)}";
    }
}
