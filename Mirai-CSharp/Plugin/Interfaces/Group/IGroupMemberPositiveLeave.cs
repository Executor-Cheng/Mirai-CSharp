using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 其他成员主动离群的接口
    /// </summary>
    public interface IGroupMemberPositiveLeave : IPlugin<IGroupMemberPositiveLeaveEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理其他成员主动离群事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot权限改变事件, 请实现 <see cref="IBotPositiveLeaveGroup"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberPositiveLeave(MiraiHttpSession session, IGroupMemberPositiveLeaveEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberPositiveLeaveEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberPositiveLeaveEventArgs e)
        {
            return GroupMemberPositiveLeave(session, e);
        }
    }
}
