using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot在群里的权限被改变的接口
    /// </summary>
    public interface IBotGroupPermissionChanged : IPlugin<IBotGroupPermissionChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot在群里的权限被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotGroupPermissionChanged(MiraiHttpSession session, IBotGroupPropertyChangedEventArgs<GroupPermission> e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotGroupPermissionChangedEventArgs>.HandleEvent(MiraiHttpSession session, IBotGroupPermissionChangedEventArgs e)
        {
            return BotGroupPermissionChanged(session, e);
        }
    }
}
