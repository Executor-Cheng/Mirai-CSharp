using Mirai_CSharp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession : IAsyncDisposable
    {
        /// <summary>
        /// Session连接状态
        /// </summary>
        public bool Connected => SessionInfo != null;
        /// <summary>
        /// Session绑定的QQ号。未连接为 <see langword="null"/>。
        /// </summary>
        public long? QQNumber => SessionInfo?.QQNumber;

        private InternalSessionInfo SessionInfo;

        private volatile bool _disposed;

        private class InternalSessionInfo
        {
            public MiraiHttpSessionOptions Options;

            public string SessionKey;

            public long QQNumber;

            public volatile CancellationTokenSource Canceller;
        }
        /// <summary>
        /// 异步释放当前Session, 并清理相关资源。
        /// <para>
        /// 本方法线程安全。
        /// </para>
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            lock (this)
            {
                if (_disposed)
                {
                    return default;
                }
                _disposed = true;
            }
            InternalSessionInfo session = Interlocked.Exchange(ref SessionInfo, null);
            if (session != null)
            {
                return new ValueTask(this.InternalReleaseAsync(session));
            }
            return default;
        }
    }
}
