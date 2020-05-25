using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot加入了一个新群的接口
    /// </summary>
    public interface IBotJoinedGroup : IPlugin<IBotJoinedGroupEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot加入了一个新群事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotJoinedGroup(MiraiHttpSession session, IBotJoinedGroupEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotJoinedGroupEventArgs>.HandleEvent(MiraiHttpSession session, IBotJoinedGroupEventArgs e)
        {
            return BotJoinedGroup(session, e);
        }
    }
}
