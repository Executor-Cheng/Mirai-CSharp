using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot被挤下线的接口
    /// </summary>
    public interface IBotKickedOffline : IPlugin<IBotKickedOfflineEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot被挤下线事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task<bool> BotKickedOffline(MiraiHttpSession session, IBotKickedOfflineEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotKickedOfflineEventArgs>.HandleEvent(MiraiHttpSession session, IBotKickedOfflineEventArgs e)
        {
            return BotKickedOffline(session, e);
        }
    }
}
