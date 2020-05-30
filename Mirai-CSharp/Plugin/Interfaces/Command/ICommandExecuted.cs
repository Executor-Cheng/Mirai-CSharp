using Mirai_CSharp.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp.Plugin.Interfaces
{
    /// <summary>
    /// 指令执行后的接口
    /// </summary>
    public interface ICommandExecuted : IPlugin<ICommandExecutedEventArgs>
    {
        /// <summary>
        /// 在类中实现时, 实现方法将处理指令执行后事件
        /// </summary>
        /// <param name="session">调用此方法的Session</param>
        /// <param name="e">Bot的QQ号</param>
        Task<bool> CommandExecuted(MiraiHttpSession session, ICommandExecutedEventArgs e);

        /// <inheritdoc/>
        Task<bool> IPlugin<ICommandExecutedEventArgs>.HandleEvent(MiraiHttpSession session, ICommandExecutedEventArgs e)
        {
            return CommandExecuted(session, e);
        }
    }
}
