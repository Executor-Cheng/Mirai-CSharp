using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 全员禁言设置被改变的接口
    /// </summary>
    public interface IGroupMuteAllChanged : IPlugin<IGroupMuteAllChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理全员禁言设置被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task GroupMuteAllChanged(IMiraiSession session, IGroupMuteAllChangedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupMuteAllChangedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupMuteAllChangedEventArgs e)
        {
            return GroupMuteAllChanged(session, e);
        }
    }
}
