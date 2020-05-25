using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 好友消息被撤回的接口
    /// </summary>
    public interface IFriendMessageRevoked : IPlugin<IFriendMessageRevokedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理好友消息被撤回事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> FriendMessageRevoked(MiraiHttpSession session, IFriendMessageRevokedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IFriendMessageRevokedEventArgs>.HandleEvent(MiraiHttpSession session, IFriendMessageRevokedEventArgs e)
        {
            return FriendMessageRevoked(session, e);
        }
    }
}
