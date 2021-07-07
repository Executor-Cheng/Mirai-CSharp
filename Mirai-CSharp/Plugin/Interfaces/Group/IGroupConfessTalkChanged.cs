using Mirai_CSharp.Models.EventArgs;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 坦白说设置被改变的接口
    /// </summary>
    public interface IGroupConfessTalkChanged : IPlugin<IGroupConfessTalkChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理坦白说设置被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task GroupConfessTalkChanged(IMiraiSession session, IGroupConfessTalkChangedEventArgs e);

        /// <inheritdoc/>
        Task IPlugin<IGroupConfessTalkChangedEventArgs>.HandleMessageAsync(IMiraiSession session, IGroupConfessTalkChangedEventArgs e)
        {
            return GroupConfessTalkChanged(session, e);
        }
    }
}
