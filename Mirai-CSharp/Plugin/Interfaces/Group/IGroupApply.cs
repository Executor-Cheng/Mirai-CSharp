using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到用户入群申请的接口
    /// </summary>
    public interface IGroupApply : IPlugin<IGroupApplyEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到用户入群申请事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupApply(MiraiHttpSession session, IGroupApplyEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupApplyEventArgs>.HandleEvent(MiraiHttpSession session, IGroupApplyEventArgs e)
        {
            return GroupApply(session, e);
        }
    }
}
