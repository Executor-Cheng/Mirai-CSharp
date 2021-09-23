using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mirai.CSharp.HttpApi.Builders;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using Mirai.CSharp.Session;
using ISharedMessageChainBuilder = Mirai.CSharp.Builders.IMessageChainBuilder;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1068 // CancellationToken parameters must come last
namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession : MiraiSession, IMiraiHttpSession, IAsyncDisposable
    {
        private static readonly HttpClient _globalClient = new HttpClient();

        private static readonly string _multipartFormdataBoundary = $"Mirai.CSharp.HttpApi/{Assembly.GetExecutingAssembly().GetName().Version}";

        /// <inheritdoc/>
        public bool Connected => _currentSession?.Connected ?? false;

        /// <inheritdoc/>
        public long? QQNumber => _currentSession?.QQNumber;

        private sealed class InternalSessionInfo : IDisposable
        {
            public Version ApiVersion { get; set; } = null!;

            public string SessionKey { get; set; } = null!;

            public long QQNumber { get; set; }

            private CancellationTokenSource? _sessionCts;

            public CancellationToken Token { get; set; }

            public bool Connected { get; set; }

            public InternalSessionInfo(CancellationToken instanceToken)
            {
                _sessionCts = CancellationTokenSource.CreateLinkedTokenSource(instanceToken);
                Token = _sessionCts.Token;
            }

            public void Dispose()
            {
                CancellationTokenSource? cts = Interlocked.Exchange(ref _sessionCts, null);
                if (cts != null)
                {
                    Connected = false;
                    cts.Cancel();
                    cts.Dispose();
                }
            }
        }

        private readonly IServiceProvider _services;

        private readonly HttpClient _client;

        private readonly MiraiHttpSessionOptions _options;

        private readonly IMiraiHttpMessageHandlerInvoker _invoker;

        private readonly JsonSerializerOptions _chatMessageSerializingOptions; // 缓存一份

        private CancellationTokenSource? _instanceCts;

        private InternalSessionInfo? _currentSession;

        /// <summary>
        /// 初始化 <see cref="MiraiHttpSession"/> 类的新实例
        /// </summary>
        public MiraiHttpSession(IServiceProvider services, IOptions<MiraiHttpSessionOptions> options, IMiraiHttpMessageHandlerInvoker invoker) : this(services, options.Value, new HttpClient(), invoker)
        {
            
        }

        /// <summary>
        /// 初始化 <see cref="MiraiHttpSession"/> 类的新实例
        /// </summary>
        protected MiraiHttpSession(IServiceProvider services, MiraiHttpSessionOptions options, HttpClient client, IMiraiHttpMessageHandlerInvoker invoker)
        {
            _services = services;
            _options = options;
            _client = client;
            _instanceCts = new CancellationTokenSource();
            _invoker = invoker;

            ChatMessageJsonConverter converter = services.GetRequiredService<ChatMessageJsonConverter>();
            JsonSerializerOptions chatMessageSerializingOptions = new JsonSerializerOptions();
            chatMessageSerializingOptions.Converters.Add(converter);
            _chatMessageSerializingOptions = chatMessageSerializingOptions;
        }

        public override ISharedMessageChainBuilder GetMessageChainBuilder()
        {
            return new MessageChainBuilder();
        }

        private static void CreateLinkedUserSessionToken(CancellationToken sessionToken,
                                                         CancellationToken userToken,
#if NETSTANDARD2_0
                                                         out CancellationTokenSource? cts,
#else
                                                         [NotNullWhen(true)]out CancellationTokenSource? cts,
#endif
                                                         out CancellationToken finalToken)
        {
            if (userToken == default)
            {
                cts = null;
                finalToken = sessionToken;
                return;
            }
            cts = CancellationTokenSource.CreateLinkedTokenSource(sessionToken, userToken);
            finalToken = cts.Token;
        }

        /// <inheritdoc/>
        public void AddPlugin(IMiraiHttpMessageHandler plugin)
        {
            CheckDisposed();
            IMiraiHttpMessageSubscriptionResolver resolver = _services.GetRequiredService<IMiraiHttpMessageSubscriptionResolver>();
            foreach (IMiraiHttpMessageSubscription? subscription in resolver.ResolveByHandler(plugin.GetType()))
            {
                subscription.AddHandler(plugin);
            }
        }

        /// <inheritdoc/>
        public void RemovePlugin(IMiraiHttpMessageHandler plugin)
        {
            CheckDisposed();
            IMiraiHttpMessageSubscriptionResolver resolver = _services.GetRequiredService<IMiraiHttpMessageSubscriptionResolver>();
            foreach (IMiraiHttpMessageSubscription? subscription in resolver.ResolveByHandler(plugin.GetType()))
            {
                subscription.RemoveHandler(plugin);
            }
        }

        public override void Dispose(bool disposing)
        {
            DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 异步释放当前会话, 并清理相关资源。
        /// </summary>
        /// <remarks>
        /// 本方法线程安全。
        /// </remarks>
        /// <returns>表示此异步操作的 <see cref="ValueTask"/></returns>
        public ValueTask DisposeAsync()
        {
            CancellationTokenSource? previousCts = Volatile.Read(ref _instanceCts);
            if (previousCts != null && Interlocked.CompareExchange(ref _instanceCts, null, previousCts) == previousCts)
            {
                GC.SuppressFinalize(this);
                return DisposeAsync(previousCts);
            }
            return default;
        }

        private async ValueTask DisposeAsync(CancellationTokenSource cts)
        {
            InternalSessionInfo? session = Volatile.Read(ref _currentSession);
            if (session != null)
            {
                try
                {
                    await InternalReleaseAsync(session);
                }
                catch
                {

                }
            }
            cts.Cancel();
            cts.Dispose();
            _client.Dispose();
        }
    }
}
