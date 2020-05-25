using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到添加好友申请的接口
    /// </summary>
    public interface INewFriendApply : IPlugin<INewFriendApplyEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到添加好友申请事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> NewFriendApply(MiraiHttpSession session, INewFriendApplyEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<INewFriendApplyEventArgs>.HandleEvent(MiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            return NewFriendApply(session, e);
        }
    }
}
