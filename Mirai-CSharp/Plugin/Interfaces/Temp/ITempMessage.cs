using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到临时消息的接口
    /// </summary>
    public interface ITempMessage : IPlugin<ITempMessageEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到临时消息事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> TempMessage(MiraiHttpSession session, ITempMessageEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<ITempMessageEventArgs>.HandleEvent(MiraiHttpSession session, ITempMessageEventArgs e)
        {
            return TempMessage(session, e);
        }
    }
}
