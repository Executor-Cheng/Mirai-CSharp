using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot主动重新登录的接口
    /// </summary>
    public interface IBotRelogin : IPlugin<IBotReloginEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot主动重新登录事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task BotRelogin(IMiraiSession session, IBotReloginEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IBotReloginEventArgs>.HandleMessageAsync(IMiraiSession session, IBotReloginEventArgs e)
        {
            return BotRelogin(session, e);
        }
    }
}
