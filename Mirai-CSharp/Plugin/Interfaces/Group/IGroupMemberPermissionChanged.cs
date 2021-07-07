using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 成员权限改变的接口
    /// </summary>
    public interface IGroupMemberPermissionChanged : IPlugin<IGroupMemberPermissionChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理成员权限改变事件
        /// </summary>
        /// <remarks>
        /// 如需处理Bot权限改变事件, 请实现 <see cref="IBotGroupPermissionChanged"/>
        /// </remarks>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task GroupMemberPermissionChanged(IMiraiSession session, IGroupMemberPermissionChangedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupMemberPermissionChangedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupMemberPermissionChangedEventArgs e)
        {
            return GroupMemberPermissionChanged(session, e);
        }
    }
}
