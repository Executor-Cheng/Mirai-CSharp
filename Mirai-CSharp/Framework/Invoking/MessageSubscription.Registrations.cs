using System.Runtime.CompilerServices;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Utility;

// see: https://source.dot.net/System.Private.CoreLib/CancellationTokenSource.cs.html#40c55f644b9860be
namespace Mirai.CSharp.Framework.Invoking
{
    public partial class MessageSubscription
    {
        protected internal sealed class Registrations
        {
            public RegistrationNode? EffictiveNodeList;

            public RegistrationNode? FreeNodeList;

            public long NextAvailableId = 1;

            private readonly UInt32Lock _lock;

            public Registrations() { }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void Recycle(RegistrationNode node)
            {
                node.Id = 0;
                node.Handler = null;

                node.Prev = null;
                node.Next = FreeNodeList;
                FreeNodeList = node;
            }

            public RegistrationNode Register(IMessageHandler handler)
            {
                RegistrationNode? node = null;
                if (FreeNodeList != null)
                {
                    EnterLock();
                    try
                    {
                        node = FreeNodeList;
                        if (node != null)
                        {
                            FreeNodeList = node.Next;

                            node.Id = NextAvailableId++;
                            node.Handler = handler;
                            node.Next = EffictiveNodeList;
                            EffictiveNodeList = node;
                            if (node.Next != null)
                            {
                                node.Next.Prev = node;
                            }
                        }
                    }
                    finally
                    {
                        ExitLock();
                    }
                }

                if (node == null)
                {
                    node = new RegistrationNode(this);
                    node.Handler = handler;
                    EnterLock();
                    try
                    {
                        node.Id = NextAvailableId++;
                        node.Next = EffictiveNodeList;
                        if (node.Next != null)
                        {
                            node.Next.Prev = node;
                        }
                        EffictiveNodeList = node;
                    }
                    finally
                    {
                        ExitLock();
                    }
                }

                return node;
            }

            public bool Unregister(long id, RegistrationNode node)
            {
                if (id == 0)
                {
                    return false;
                }
                EnterLock();
                try
                {
                    if (node.Id != id)
                    {
                        return false;
                    }
                    if (EffictiveNodeList == node)
                    {
                        EffictiveNodeList = node.Next;
                    }
                    else
                    {
                        node.Prev!.Next = node.Next;
                    }
                    if (node.Next != null)
                    {
                        node.Next.Prev = node.Prev;
                    }
                    Recycle(node);
                    return true;
                }
                finally
                {
                    ExitLock();
                }
            }

            public void UnregisterAll()
            {
                EnterLock();
                try
                {
                    RegistrationNode? node = EffictiveNodeList;
                    EffictiveNodeList = null;
                    while (node != null)
                    {
                        RegistrationNode? next = node.Next;
                        Recycle(node);
                        node = next;
                    }
                }
                finally
                {
                    ExitLock();
                }
            }

            public void EnterLock()
            {
                _lock.EnterWriteLock();
            }

            public void ExitLock()
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
