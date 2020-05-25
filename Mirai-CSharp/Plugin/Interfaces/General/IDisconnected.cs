using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 与mirai-api-http的ws连接被异常断开的接口
    /// </summary>
    public interface IDisconnected : IPlugin<IDisconnectedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理与mirai-api-http的ws连接被异常断开事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> Disconnected(MiraiHttpSession session, IDisconnectedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IDisconnectedEventArgs>.HandleEvent(MiraiHttpSession session, IDisconnectedEventArgs e)
        {
            return Disconnected(session, e);
        }
    }
}
