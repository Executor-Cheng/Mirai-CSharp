using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IGroupApplyEventArgs : INewApplyEventArgs
    {
        [JsonPropertyName("groupName")]
        string FromGroupName { get; }
    }

    public class GroupApplyEventArgs : NewApplyEventArgs, IGroupApplyEventArgs
    {
        [JsonPropertyName("groupName")]
        public string FromGroupName { get; set; }

        public GroupApplyEventArgs()
        {

        }

        public GroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName) : base(eventId, fromGroup, fromQQ, nickName)
        {
            FromGroupName = fromGroupName;
        }
    }
}
