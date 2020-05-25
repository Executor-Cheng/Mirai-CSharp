using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot登录成功的接口
    /// </summary>
    public interface IBotOnline : IPlugin<IBotOnlineEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot登录成功事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task<bool> BotOnline(MiraiHttpSession session, IBotOnlineEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotOnlineEventArgs>.HandleEvent(MiraiHttpSession session, IBotOnlineEventArgs e)
        {
            return BotOnline(session, e);
        }
    }
}
