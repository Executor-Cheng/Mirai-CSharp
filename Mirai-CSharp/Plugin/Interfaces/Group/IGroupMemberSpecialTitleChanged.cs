using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 群头衔改动的接口
    /// </summary>
    public interface IGroupMemberSpecialTitleChanged : IPlugin<IGroupMemberSpecialTitleChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理群头衔改动事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupMemberSpecialTitleChanged(MiraiHttpSession session, IGroupMemberSpecialTitleChangedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupMemberSpecialTitleChangedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupMemberSpecialTitleChangedEventArgs e)
        {
            return GroupMemberSpecialTitleChanged(session, e);
        }
    }
}
