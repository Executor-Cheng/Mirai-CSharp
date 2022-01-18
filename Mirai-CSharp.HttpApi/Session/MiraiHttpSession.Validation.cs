using System;
using System.Threading;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        protected void CheckDisposed()
        {
            if (Volatile.Read(ref _instanceCts) == null)
            {
                throw new ObjectDisposedException(nameof(MiraiHttpSession));
            }
        }

        protected InternalSessionInfo SafeGetSession()
        {
            CheckDisposed();
            InternalSessionInfo? session = _currentSession;
            if (session == null || !session.Connected)
            {
                throw new InvalidOperationException("请先连接到一个Session。");
            }
            return session;
        }
    }
}
