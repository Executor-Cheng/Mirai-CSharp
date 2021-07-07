using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai_CSharp.Framework.Clients;
using Mirai_CSharp.Framework.Handlers;
using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Framework.Invoking
{
    public class MessageSubscription<TMessage> : IMessageSubscription<TMessage> where TMessage : IMessage
    {
        protected virtual IEnumerable<IMessageHandler<TMessage>> Handlers { get; }

        public MessageSubscription(IEnumerable<IMessageHandler> handlers)
        {
            Handlers = ResolveHandlers(handlers);
        }

        protected virtual IEnumerable<IMessageHandler<TMessage>> ResolveHandlers(IEnumerable<IMessageHandler> handlers)
        {
            List<IMessageHandler<TMessage>> filteredHandlers = new List<IMessageHandler<TMessage>>();
            var expectedHandler = typeof(IContravarianceMessageHandler<TMessage>);
            var expectedInvarianceHandler = typeof(IInvarianceMessageHandler<TMessage>);
            foreach (IMessageHandler handler in handlers)
            {
                if (expectedHandler.IsAssignableFrom(handler.GetType()) ||
                    expectedInvarianceHandler.IsAssignableFrom(handler.GetType()))
                {
                    filteredHandlers.Add((IMessageHandler<TMessage>)handler);
                }
            }
            return filteredHandlers.ToArray();
        }

        public virtual async Task HandleMessageAsync(IMessageClient client, TMessage message)
        {
            foreach (IMessageHandler<TMessage> handler in Handlers)
            {
                await handler.HandleMessageAsync(client, message);
            }
        }
    }
}
