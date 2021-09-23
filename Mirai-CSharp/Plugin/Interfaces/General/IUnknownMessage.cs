using Mirai.CSharp.Models;
using System.Threading.Tasks;

namespace Mirai.CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到未知消息的接口
    /// </summary>
    public interface IUnknownMessage : IPlugin<IUnknownMessageEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到未知消息事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> UnknownMessage(MiraiHttpSession session, IUnknownMessageEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IUnknownMessageEventArgs>.HandleEvent(MiraiHttpSession session, IUnknownMessageEventArgs e)
        {
            return UnknownMessage(session, e);
        }
    }
}
