using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot意外断开连接的接口
    /// </summary>
    public interface IBotDropped : IPlugin<IBotDroppedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot意外断开连接事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task<bool> BotDropped(MiraiHttpSession session, IBotDroppedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotDroppedEventArgs>.HandleEvent(MiraiHttpSession session, IBotDroppedEventArgs e)
        {
            return BotDropped(session, e);
        }
    }
}
