using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot主动离线的接口
    /// </summary>
    public interface IBotPositiveOffline : IPlugin<IBotPositiveOfflineEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot主动离线事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task<bool> BotPositiveOffline(MiraiHttpSession session, IBotPositiveOfflineEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotPositiveOfflineEventArgs>.HandleEvent(MiraiHttpSession session, IBotPositiveOfflineEventArgs e)
        {
            return BotPositiveOffline(session, e);
        }
    }
}
