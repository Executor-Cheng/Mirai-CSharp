using Mirai_CSharp.Models;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 收到临时消息
        /// </summary>
        public event CommonEventHandler<ITempMessageEventArgs>? TempMessageEvt;
    }
}
