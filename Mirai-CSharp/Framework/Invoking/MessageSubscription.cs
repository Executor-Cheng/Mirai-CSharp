using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Framework.Models.General;
using Mirai.CSharp.Utility;

namespace Mirai.CSharp.Framework.Invoking
{
    public class MessageSubscription<TClient, TMessage> : MessageHandler<TClient, TMessage>, IMessageSubscription<TClient, TMessage> where TClient : IMessageClient
                                                                                                                                     where TMessage : IMessage
    {
        protected IMessageHandler<TClient, TMessage>[] StaticHandlers { get; }

        protected IList<IMessageHandler<TClient, TMessage>> DynamicHandlers { get; }

        protected uint _dynamicHandlerVersion;

        // [0]: Writer [1-31]: Reader
        protected UInt32Lock _dynamicHandlerLock;

        public MessageSubscription(IEnumerable<IMessageHandler> handlers)
        {
            if (handlers.Any())
            {
                StaticHandlers = ResolveStaticHandlers(new LinkedList<IMessageHandler>(handlers), new List<IMessageHandler<TClient, TMessage>>());
            }
            else
            {
                StaticHandlers = Array.Empty<IMessageHandler<TClient, TMessage>>();
            }
            DynamicHandlers = new List<IMessageHandler<TClient, TMessage>>();
        }

        protected virtual IMessageHandler<TClient, TMessage>[] ResolveStaticHandlers(LinkedList<IMessageHandler> handlers, List<IMessageHandler<TClient, TMessage>> filtered)
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
                        filtered.Add((IMessageHandler<TClient, TMessage>)handler);
                        handlers.Remove(handlerNode);
                    }
                }
            }
            return filtered.ToArray();
        }

        public virtual void AddHandler(IMessageHandler handler)
        {
            AddHandler((IMessageHandler<TClient, TMessage>)handler);
        }

        public virtual void RemoveHandler(IMessageHandler handler)
        {
            RemoveHandler((IMessageHandler<TClient, TMessage>)handler);
        }

        public virtual void AddHandler(IMessageHandler<TClient, TMessage> handler)
        {
            _dynamicHandlerLock.EnterWriteLock();
            DynamicHandlers.Add(handler);
            _dynamicHandlerVersion++;
            _dynamicHandlerLock.ExitWriteLock();
        }

        public virtual void RemoveHandler(IMessageHandler<TClient, TMessage> handler)
        {
            _dynamicHandlerLock.EnterWriteLock();
            DynamicHandlers.Remove(handler);
            _dynamicHandlerVersion++;
            _dynamicHandlerLock.ExitWriteLock();
        }

        public override async Task HandleMessageAsync(TClient client, TMessage message)
        {
            foreach (IMessageHandler<TClient, TMessage> handler in this)
            {
                await handler.HandleMessageAsync(client, message);
                if (message.BlockRemainingHandlers)
                {
                    break;
                }
            }
        }

        public override Task HandleMessageAsync(IMessageClient client, IMessage message)
        {
            return HandleMessageAsync((TClient)client, (TMessage)message);
        }

        public IEnumerator<IMessageHandler<TClient, TMessage>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator();
        }

        public struct Enumerator : IEnumerator<IMessageHandler<TClient, TMessage>>
        {
            private readonly MessageSubscription<TClient, TMessage> _subscription;

            private readonly uint _version;

            private IMessageHandler<TClient, TMessage>? _current;

            private int _staticIdx;

            private int _dynamicIdx;

            public Enumerator(MessageSubscription<TClient, TMessage> subscription)
            {
                _subscription = subscription;
                _current = null;
                _version = subscription._dynamicHandlerVersion;
                _staticIdx = 0;
                _dynamicIdx = 0;
            }

            public IMessageHandler<TClient, TMessage> Current => _current!;

            object IEnumerator.Current => _current!;

            public bool MoveNext()
            {
                if (_staticIdx < _subscription.StaticHandlers.Length)
                {
                    _current = _subscription.StaticHandlers[_staticIdx++];
                    return true;
                }
                // 先行判断 DynamicHandlers 有没有被修改, 已修改就不申请读锁
                if (_version == _subscription._dynamicHandlerVersion)
                {
                    _subscription._dynamicHandlerLock.EnterReadLock();
                    // 再判断一次 DynamicHandlers 有没有被修改, 不排除 EnterReadLock 时已有一个 Writer 在操作 DynamicHandlers
                    if (_version == _subscription._dynamicHandlerVersion && _dynamicIdx < _subscription.DynamicHandlers.Count)
                    {
                        _current = _subscription.DynamicHandlers[_dynamicIdx++];
                        _subscription._dynamicHandlerLock.ExitReadLock();
                        return true;
                    }
                    _subscription._dynamicHandlerLock.ExitReadLock();
                }
                return false;
            }

            public void Reset()
            {
                _current = null;
                _staticIdx = 0;
                _dynamicIdx = 0;
            }

            public void Dispose()
            {

            }
        }
    }
}
