using Mirai_CSharp.Models;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 指令执行后引发的事件
        /// </summary>
        public event CommonEventHandler<ICommandExecutedEventArgs>? CommandExecuted;
    }
}
