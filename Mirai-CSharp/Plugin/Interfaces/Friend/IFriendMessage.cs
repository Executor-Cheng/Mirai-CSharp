using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 收到好友消息的接口
    /// </summary>
    public interface IFriendMessage : IPlugin<IFriendMessageEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理收到好友消息事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> FriendMessage(MiraiHttpSession session, IFriendMessageEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IFriendMessageEventArgs>.HandleEvent(MiraiHttpSession session, IFriendMessageEventArgs e)
        {
            return FriendMessage(session, e);
        }
    }
}
