using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请相关信息的接口。继承自 <see cref="ICommonGroupApplyEventArgs"/>
    /// </summary>
    public interface IGroupApplyEventArgs : ICommonGroupApplyEventArgs
    {

    }

    public class GroupApplyEventArgs : CommonGroupApplyEventArgs, IGroupApplyEventArgs
    {
        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName) : base(fromGroupName, eventId, fromGroup, fromQQ, nickName)
        {

        }
    }
}
