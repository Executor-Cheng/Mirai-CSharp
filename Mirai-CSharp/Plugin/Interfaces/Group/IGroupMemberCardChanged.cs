using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 群名片改动的接口
    /// </summary>
    public interface IGroupMemberCardChanged : IPlugin<IGroupMemberCardChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理群名片改动事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task GroupMemberCardChanged(IMiraiSession session, IGroupMemberCardChangedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupMemberCardChangedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupMemberCardChangedEventArgs e)
        {
            return GroupMemberCardChanged(session, e);
        }
    }
}
