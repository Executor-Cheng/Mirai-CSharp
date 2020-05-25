using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot主动退出一个群的接口
    /// </summary>
    public interface IBotPositiveLeaveGroup : IPlugin<IBotPositiveLeaveGroupEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot主动退出一个群事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotPositiveLeaveGroup(MiraiHttpSession session, IBotPositiveLeaveGroupEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotPositiveLeaveGroupEventArgs>.HandleEvent(MiraiHttpSession session, IBotPositiveLeaveGroupEventArgs e)
        {
            return BotPositiveLeaveGroup(session, e);
        }
    }
}
