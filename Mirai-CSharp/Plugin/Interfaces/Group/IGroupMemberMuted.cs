using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 其他成员被禁言的接口
    /// </summary>
    public interface IGroupMemberMuted : IPlugin<IGroupMemberMutedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理其他成员被禁言事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot被禁言事件, 请实现 <see cref="IBotMuted"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberMuted(MiraiHttpSession session, IGroupMemberMutedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberMutedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberMutedEventArgs e)
        {
            return GroupMemberMuted(session, e);
        }
    }
}
