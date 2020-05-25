using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot被禁言的接口
    /// </summary>
    public interface IBotMuted : IPlugin<IBotMutedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot被禁言事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotMuted(MiraiHttpSession session, IBotMutedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotMutedEventArgs>.HandleEvent(MiraiHttpSession session, IBotMutedEventArgs e)
        {
            return BotMuted(session, e);
        }
    }
}
