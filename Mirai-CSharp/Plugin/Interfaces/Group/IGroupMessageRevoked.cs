using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 群消息被撤回的接口
    /// </summary>
    public interface IGroupMessageRevoked : IPlugin<IGroupMessageRevokedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理群消息被撤回事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMessageRevoked(MiraiHttpSession session, IGroupMessageRevokedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMessageRevokedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMessageRevokedEventArgs e)
        {
            return GroupMessageRevoked(session, e);
        }
    }
}
