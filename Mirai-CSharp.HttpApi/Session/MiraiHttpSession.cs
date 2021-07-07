using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession : IAsyncDisposable
    {
        private static readonly HttpClient _Client = new HttpClient();

        /// <summary>
        /// Session连接状态
        /// </summary>
        public bool Connected => SessionInfo?.Connected ?? false;
        /// <summary>
        /// Session绑定的QQ号。未连接为 <see langword="null"/>。
        /// </summary>
        public long? QQNumber => SessionInfo?.QQNumber;

        private InternalSessionInfo? SessionInfo;

        private ImmutableList<IPlugin> Plugins = Array.Empty<IPlugin>().ToImmutableList();

        private volatile bool _disposed;

        private class InternalSessionInfo
        {
            public MiraiHttpSessionOptions Options = null!;

            public HttpClient Client = null!;

            public Version ApiVersion = null!;

            public string SessionKey = null!;

            public long QQNumber;

            public CancellationToken Token;

            public CancellationTokenSource Canceller = null!;

            public bool Connected;
        }

        /// <summary>
        /// 初始化 <see cref="MiraiHttpSession"/> 类的新实例
        /// </summary>
        public MiraiHttpSession() { }

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
        /// </summary>
        /// <remarks>
        /// 本方法线程安全。
        /// </remarks>
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
            InternalSessionInfo? session = Interlocked.Exchange(ref SessionInfo, null);
            if (session != null)
            {
                Plugins = null!;
                foreach (FieldInfo eventField in typeof(MiraiHttpSession).GetEvents().Select(p => typeof(MiraiHttpSession).GetField(p.Name, BindingFlags.NonPublic | BindingFlags.Instance)!))
                {
                    eventField.SetValue(this, null); // 用反射解决掉所有事件的Handler
                }
                return new ValueTask(InternalReleaseAsync(session));
            }
            return default;
        }
    }
}
