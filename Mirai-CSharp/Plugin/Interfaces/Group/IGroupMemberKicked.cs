using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 其他成员被踢出群的接口
    /// </summary>
    public interface IGroupMemberKicked : IPlugin<IGroupMemberKickedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理其他成员被踢出群事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot被踢出群事件, 请实现 <see cref="IBotKickedOut"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberKicked(MiraiHttpSession session, IGroupMemberKickedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberKickedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberKickedEventArgs e)
        {
            return GroupMemberKicked(session, e);
        }
    }
}
