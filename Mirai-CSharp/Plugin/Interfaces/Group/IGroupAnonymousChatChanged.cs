using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 匿名聊天设置被改变的接口
    /// </summary>
    public interface IGroupAnonymousChatChanged : IPlugin<IGroupAnonymousChatChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理匿名聊天设置被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupAnonymousChatChanged(MiraiHttpSession session, IGroupAnonymousChatChangedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupAnonymousChatChangedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupAnonymousChatChangedEventArgs e)
        {
            return GroupAnonymousChatChanged(session, e);
        }
    }
}
