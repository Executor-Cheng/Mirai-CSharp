using System.Collections;
using System.Collections.Generic;
using Mirai.CSharp.Framework.Handlers;

namespace Mirai.CSharp.Framework.Invoking
{
    public partial class MessageSubscription
    {
        public struct Enumerator : IEnumerator<IMessageHandler>
        {
            private readonly MessageSubscription _subscription;

            private IMessageHandler? _current;

            private int _staticIdx;

            private RegistrationNode? _dynamicNode;

            public Enumerator(MessageSubscription subscription)
            {
                _subscription = subscription;
                _current = null;
                _staticIdx = 0;
                _dynamicNode = subscription._registrations?.EffictiveNodeList;
            }

            public IMessageHandler Current => _current!;

            object IEnumerator.Current => _current!;

            public bool MoveNext()
            {
                if (_staticIdx < _subscription.StaticHandlers.Length)
                {
                    _current = _subscription.StaticHandlers[_staticIdx++];
                    return true;
                }
                if (_dynamicNode != null)
                {
                    _current = _dynamicNode.Handler;
                    _dynamicNode = _dynamicNode.Next;
                    return _current != null;
                }
                return false;
            }

            public void Reset()
            {
                _current = null;
                _staticIdx = 0;
                _dynamicNode = _subscription._registrations?.EffictiveNodeList;
            }

            public void Dispose()
            {

            }
        }
    }
}
