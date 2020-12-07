using Mirai_CSharp.Models;
using System;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 通用事件委托
        /// </summary>
        /// <typeparam name="TEventArgs">事件参数</typeparam>
        /// <param name="sender">调用此委托的 <see cref="MiraiHttpSession"/></param>
        /// <param name="e">事件参数</param>
        public delegate Task<bool> CommonEventHandler<TEventArgs>(MiraiHttpSession sender, TEventArgs e);
        /// <summary>
        /// 与mirai-api-http的ws连接被异常断开
        /// </summary>
        public event CommonEventHandler<IDisconnectedEventArgs>? DisconnectedEvt;
        /// <summary>
        /// 收到未知消息。如有需要, 请自行解析
        /// </summary>
        public event CommonEventHandler<IUnknownMessageEventArgs>? UnknownMessageEvt; 
    }
}
