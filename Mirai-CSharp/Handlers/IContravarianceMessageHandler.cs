using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    public interface IContravarianceMiraiMessageHandler<in TClient, in TMessage> : IMiraiMessageHandlerBase<TClient, TMessage>, IContravarianceMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                                                 where TMessage : IMiraiMessage
    {

    }
}
