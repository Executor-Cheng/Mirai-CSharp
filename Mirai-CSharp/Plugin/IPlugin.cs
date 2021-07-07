using System.Threading.Tasks;
using Mirai_CSharp.Framework.Clients;
using Mirai_CSharp.Framework.Handlers;
using Mirai_CSharp.Models.EventArgs;

namespace Mirai_CSharp.Plugin
{
    /// <summary>
    /// 表示处理mirai-api-http消息的接口
    /// </summary>
    public interface IPlugin : IMessageHandler
    {

    }

    /// <summary>
    /// 内部使用。表示处理特定 <typeparamref name="TEventArgs"/> 的接口
    /// </summary>
    public interface IPlugin<TEventArgs> : IPlugin, IMessageHandler<TEventArgs> where TEventArgs : IEventArgsBase
    {
        Task HandleMessageAsync(IMiraiSession session, TEventArgs e)
            => HandleMessageAsync((IMessageClient)session, e);
    }
}
