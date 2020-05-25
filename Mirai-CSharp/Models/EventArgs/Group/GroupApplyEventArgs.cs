using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供入群申请相关信息的接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
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
