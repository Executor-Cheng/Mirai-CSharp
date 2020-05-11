using Mirai_CSharp.Utility.JsonConverters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IGroupMessageEventArgs : ICommonMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        IGroupMemberInfo Sender { get; }
    }

    public class GroupMessageEventArgs : CommonMessageEventArgs, IGroupMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<GroupMemberInfo, IGroupMemberInfo>))]
        [JsonPropertyName("sender")]
        public IGroupMemberInfo Sender { get; set; }

        public GroupMessageEventArgs() { }

        public GroupMessageEventArgs(IMessageBase[] chain, IGroupMemberInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<MessageBase>)Chain)}";
    }
}
