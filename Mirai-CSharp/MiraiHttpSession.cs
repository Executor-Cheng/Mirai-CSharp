using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin;
using System;
using System.Collections.Immutable;
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

        private Version ApiVersion;

        private InternalSessionInfo SessionInfo;

        private ImmutableList<IPlugin> Plugins = Array.Empty<IPlugin>().ToImmutableList();

        private volatile bool _disposed;

        private class InternalSessionInfo
        {
            public MiraiHttpSessionOptions Options;

            public string SessionKey;

            public long QQNumber;

            public volatile CancellationTokenSource Canceller;
        }

        /// <summary>
        /// 添加一个用于处理消息的 <see cref="IPlugin"/>
        /// </summary>
        public void AddPlugin(IPlugin plugin)
        {
            CheckDisposed();
            Plugins = Plugins.Add(plugin);
        }

        /// <summary>
        /// 移除一个用于处理消息的 <see cref="IPlugin"/>。 <paramref name="plugin"/> 必须在之前通过 <see cref="AddPlugin(IPlugin)"/> 添加过
        /// </summary>
        public void RemovePlugin(IPlugin plugin)
        {
            CheckDisposed();
            Plugins = Plugins.Remove(plugin);
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
