using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 其他成员被取消禁言的接口
    /// </summary>
    public interface IGroupMemberUnmuted : IPlugin<IGroupMemberUnmutedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理其他成员被取消禁言事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot被取消禁言事件, 请实现 <see cref="IBotUnmuted"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberUnmuted(MiraiHttpSession session, IGroupMemberUnmutedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberUnmutedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberUnmutedEventArgs e)
        {
            return GroupMemberUnmuted(session, e);
        }
    }
}
