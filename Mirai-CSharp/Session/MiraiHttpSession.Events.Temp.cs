using Mirai.CSharp.Models;

namespace Mirai.CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 收到临时消息
        /// </summary>
        public event CommonEventHandler<ITempMessageEventArgs>? TempMessageEvt;
    }
}
