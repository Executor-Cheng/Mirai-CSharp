using System.Threading.Tasks;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    /// <summary>
    /// 本接口默认<strong>不</strong>处理任何的 <see cref="IMiraiMessage"/>
    /// </summary>
    /// <remarks>
    /// 一般情况下应当实现 <see cref="IMiraiMessageHandler{TClient, TMessage}"/>
    /// </remarks>
    public interface IMiraiMessageHandler : IMessageHandler
    {
#if !NETSTANDARD2_0
        Task HandleMessageAsync(IMiraiSession session, IMiraiMessage message)
        {
            return _DefaultImplTask;
        }

        Task IMessageHandler.HandleMessageAsync(Framework.Clients.IMessageClient client, Framework.Models.General.IMessage message)
        {
            return HandleMessageAsync((IMiraiSession)client, (IMiraiMessage)message);
        }
#else
        Task HandleMessageAsync(IMiraiSession client, IMiraiMessage message);
#endif
    }
}
