using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 群员邀请好友加群设置被改变的接口
    /// </summary>
    public interface IGroupMemberInviteChanged : IPlugin<IGroupMemberInviteChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理群员邀请好友加群设置被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberInviteChanged(MiraiHttpSession session, IGroupMemberInviteChangedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberInviteChangedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberInviteChangedEventArgs e)
        {
            return GroupMemberInviteChanged(session, e);
        }
    }
}
