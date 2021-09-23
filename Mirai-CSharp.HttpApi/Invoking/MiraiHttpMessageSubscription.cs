using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Invoking;

namespace Mirai.CSharp.HttpApi.Invoking
{
    public class MiraiHttpMessageSubscription<TMessage> : MiraiMessageSubscription<IMiraiHttpSession, TMessage>, IMiraiHttpMessageSubscription<TMessage> where TMessage : IMiraiHttpMessage
    {
        public MiraiHttpMessageSubscription(IEnumerable<IMessageHandler> base2Handlers, IEnumerable<IMiraiMessageHandler> base1handlers, IEnumerable<IMiraiHttpMessageHandler> handlers) : base(handlers.Concat(base1handlers).Concat(base2Handlers))
        {
            
        }

        public MiraiHttpMessageSubscription(IEnumerable<IMessageHandler> handlers) : base(handlers)
        {

        }

        protected override IMessageHandler<IMiraiHttpSession, TMessage>[] ResolveStaticHandlers(LinkedList<IMessageHandler> handlers, List<IMessageHandler<IMiraiHttpSession, TMessage>> filtered)
        {
            if (handlers.Count != 0)
            {
                var expectedHandler = typeof(IContravarianceMiraiHttpMessageHandler<TMessage>);
                var expectedInvarianceHandler = typeof(IInvarianceMiraiHttpMessageHandler<TMessage>);
                for (LinkedListNode<IMessageHandler>? handlerNode = handlers.First; handlerNode != null; handlerNode = handlerNode.Next)
                {
                    IMessageHandler handler = handlerNode.Value;
                    if (expectedHandler.IsAssignableFrom(handler.GetType()) ||
                        expectedInvarianceHandler.IsAssignableFrom(handler.GetType()))
                    {
                        filtered.Add((IMiraiHttpMessageHandlerBase<TMessage>)handler);
                        handlers.Remove(handlerNode);
                    }
                }
            }
            return base.ResolveStaticHandlers(handlers, filtered);
        }

#if NETSTANDARD2_0
        public virtual Task HandleMessageAsync(IMiraiHttpSession session, IMiraiHttpMessage message)
        {
            return base.HandleMessageAsync(client: session, (TMessage)message);
        }
#endif
    }
}
