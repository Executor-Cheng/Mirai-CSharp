using System;
using System.Threading;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        private void CheckDisposed()
        {
            if (Volatile.Read(ref _instanceCts) == null)
            {
                throw new ObjectDisposedException(nameof(MiraiHttpSession));
            }
        }

        private InternalSessionInfo SafeGetSession()
        {
            CheckDisposed();
            InternalSessionInfo? session = _currentSession;
            return session ?? throw new InvalidOperationException("请先连接到一个Session。");
        }
    }
}
