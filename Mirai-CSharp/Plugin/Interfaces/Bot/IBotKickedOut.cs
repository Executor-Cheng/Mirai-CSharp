using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// Bot被踢出一个群的接口
    /// </summary>
    public interface IBotKickedOut : IPlugin<IBotKickedOutEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理Bot被踢出一个群事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> BotKickedOut(MiraiHttpSession session, IBotKickedOutEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IBotKickedOutEventArgs>.HandleEvent(MiraiHttpSession session, IBotKickedOutEventArgs e)
        {
            return BotKickedOut(session, e);
        }
    }
}
