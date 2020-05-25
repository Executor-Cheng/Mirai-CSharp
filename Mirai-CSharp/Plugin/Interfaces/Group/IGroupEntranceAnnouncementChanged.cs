using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 某群入群公告改变的接口
    /// </summary>
    public interface IGroupEntranceAnnouncementChanged : IPlugin<IGroupEntranceAnnouncementChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理某群入群公告改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupEntranceAnnouncementChanged(MiraiHttpSession session, IGroupPropertyChangedEventArgs<string> e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupEntranceAnnouncementChangedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupEntranceAnnouncementChangedEventArgs e)
        {
            return GroupEntranceAnnouncementChanged(session, e);
        }
    }
}
