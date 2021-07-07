using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot被邀请入群的接口
    /// </summary>
    public interface IBotInvitedJoinGroup : IPlugin<IBotInvitedJoinGroupEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot被邀请入群事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task BotInvitedJoinGroup(IMiraiSession session, IBotInvitedJoinGroupEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IBotInvitedJoinGroupEventArgs>.HandleMessageAsync(IMiraiSession session, IBotInvitedJoinGroupEventArgs e)
        {
            return BotInvitedJoinGroup(session, e);
        }
    }
}
