using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到群消息的接口
    /// </summary>
    public interface IGroupMessage : IPlugin<IGroupMessageEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到群消息事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMessageEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            return GroupMessage(session, e);
        }
    }
}
