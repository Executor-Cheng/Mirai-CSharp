using System;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供入群申请/受邀入群相关信息的基接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
    public interface ICommonGroupApplyEventArgs : INewApplyEventArgs
    {
        [JsonPropertyName("groupName")]
        string FromGroupName { get; }
    }

    public class CommonGroupApplyEventArgs : NewApplyEventArgs, 
                                       IGroupApplyEventArgs,
                                       IBotInvitedJoinGroupEventArgs
    {
        [JsonPropertyName("groupName")]
        public string FromGroupName { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommonGroupApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommonGroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName) : base(eventId, fromGroup, fromQQ, nickName)
        {
            FromGroupName = fromGroupName;
        }
    }
}
