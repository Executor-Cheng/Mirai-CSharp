using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot被取消禁言的接口
    /// </summary>
    public interface IBotUnmuted : IPlugin<IBotUnmutedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot被取消禁言事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotUnmuted(MiraiHttpSession session, IBotUnmutedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotUnmutedEventArgs>.HandleEvent(MiraiHttpSession session, IBotUnmutedEventArgs e)
        {
            return BotUnmuted(session, e);
        }
    }
}
