using Mirai_CSharp.Models.EventArgs;
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
        Task GroupMemberMuted(IMiraiSession session, IGroupMemberMutedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupMemberMutedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupMemberMutedEventArgs e)
        {
            return GroupMemberMuted(session, e);
        }
    }
}
