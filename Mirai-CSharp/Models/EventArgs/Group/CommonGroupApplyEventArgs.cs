using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请/受邀入群相关信息的基接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
    public interface ICommonGroupApplyEventArgs : INewApplyEventArgs
    {
        /// <summary>
        /// 来源群名称
        /// </summary>
        string FromGroupName { get; }
    }

    public abstract class CommonGroupApplyEventArgs : NewApplyEventArgs, ICommonGroupApplyEventArgs
    {
        /// <inheritdoc/>
        public string FromGroupName { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected CommonGroupApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected CommonGroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName) : base(eventId, fromGroup, fromQQ, nickName)
        {
            FromGroupName = fromGroupName;
        }
    }
}
