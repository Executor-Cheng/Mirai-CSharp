#if !NETSTANDARD2_0
using System.Threading.Tasks;
#endif
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Framework.Models.General;
using System.Collections.Generic;

namespace Mirai.CSharp.Framework.Invoking
{
    public interface IMessageSubscription : IMessageHandler
    {
        void AddHandler(IMessageHandler handler);

        void RemoveHandler(IMessageHandler handler);
    }

    public interface IMessageSubscription<TClient, TMessage> : IMessageSubscription,
                                                               IMessageHandler<TClient, TMessage>,
                                                               IEnumerable<IMessageHandler<TClient, TMessage>> where TClient : IMessageClient // 只允许实现一种泛型接口
                                                                                                               where TMessage : IMessage // 想实现多种的请自己解决CS8705
    {
        void AddHandler(IMessageHandler<TClient, TMessage> handler);

        void RemoveHandler(IMessageHandler<TClient, TMessage> handler);

#if !NETSTANDARD2_0
        Task IMessageHandler.HandleMessageAsync(IMessageClient client, IMessage message)
            => HandleMessageAsync((TClient)client, (TMessage)message);
#endif
    }
}
