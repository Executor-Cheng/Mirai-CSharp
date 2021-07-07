using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 新人入群的接口
    /// </summary>
    public interface IGroupMemberJoined : IPlugin<IGroupMemberJoinedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理新人入群事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot入群事件, 请实现 <see cref="IBotJoinedGroup"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task GroupMemberJoined(IMiraiSession session, IGroupMemberJoinedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupMemberJoinedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupMemberJoinedEventArgs e)
        {
            return GroupMemberJoined(session, e);
        }
    }
}
