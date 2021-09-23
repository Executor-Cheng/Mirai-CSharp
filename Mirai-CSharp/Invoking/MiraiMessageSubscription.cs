using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Invoking
{
    public class MiraiMessageSubscription<TClient, TMessage> : MessageSubscription<TClient, TMessage>, IMiraiMessageSubscription<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                    where TMessage : IMiraiMessage
    {
        public MiraiMessageSubscription(IEnumerable<IMessageHandler> baseHandlers, IEnumerable<IMiraiMessageHandler> handlers) : base(handlers.Concat(baseHandlers))
        {
            
        }

        public MiraiMessageSubscription(IEnumerable<IMessageHandler> handlers) : base(handlers)
        {

        }

        protected override IMessageHandler<TClient, TMessage>[] ResolveStaticHandlers(LinkedList<IMessageHandler> handlers, List<IMessageHandler<TClient, TMessage>> filtered)
        {
            if (handlers.Count != 0)
            {
                var expectedHandler = typeof(IContravarianceMessageHandler<TClient, TMessage>);
                var expectedInvarianceHandler = typeof(IInvarianceMessageHandler<TClient, TMessage>);
                for (LinkedListNode<IMessageHandler>? handlerNode = handlers.First; handlerNode != null; handlerNode = handlerNode.Next)
                {
                    IMessageHandler handler = handlerNode.Value;
                    if (expectedHandler.IsAssignableFrom(handler.GetType()) ||
                        expectedInvarianceHandler.IsAssignableFrom(handler.GetType()))
                    {
                        filtered.Add((IMiraiMessageHandlerBase<TClient, TMessage>)handler);
                        handlers.Remove(handlerNode);
                    }
                }
            }
            return base.ResolveStaticHandlers(handlers, filtered);
        }

        public virtual Task HandleMessageAsync(IMiraiSession session, IMiraiMessage message)
        {
            return base.HandleMessageAsync((TClient)session, (TMessage)message);
        }
    }
}
