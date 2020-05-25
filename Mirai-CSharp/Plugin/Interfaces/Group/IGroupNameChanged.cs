using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 某个群名被改变的接口
    /// </summary>
    public interface IGroupNameChanged : IPlugin<IGroupNameChangedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理某个群名被改变事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">事件信息</param>
        Task<bool> GroupNameChanged(MiraiHttpSession session, IGroupNameChangedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<IGroupNameChangedEventArgs>.HandleEvent(MiraiHttpSession session, IGroupNameChangedEventArgs e)
        {
            return GroupNameChanged(session, e);
        }
    }
}
