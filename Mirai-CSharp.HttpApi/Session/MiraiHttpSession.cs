using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.HttpApi.Builders;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Utility;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using Mirai.CSharp.Session;
using ISharedMessageChainBuilder = Mirai.CSharp.Builders.IMessageChainBuilder;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1068 // CancellationToken parameters must come last
namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession : MiraiSession, IMiraiHttpSession, IAsyncDisposable
    {
        protected static readonly HttpClient _globalClient = new HttpClient();

        protected static readonly string _multipartFormdataBoundary = $"Mirai.CSharp.HttpApi/{Assembly.GetExecutingAssembly().GetName().Version}";

        /// <inheritdoc/>
        public bool Connected => _currentSession?.Connected ?? false;

        /// <inheritdoc/>
        public long? QQNumber => _currentSession?.QQNumber;

        protected sealed class InternalSessionInfo : IDisposable
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

        protected readonly IServiceProvider _services;

        protected readonly HttpClient _client;

        protected readonly MiraiHttpSessionOptions _options;

        protected readonly IMiraiHttpMessageHandlerInvoker _invoker;

        protected readonly JsonSerializerOptions _chatMessageSerializingOptions; // 缓存一份

        protected CancellationTokenSource? _instanceCts;

        protected InternalSessionInfo? _currentSession;

        /// <summary>
        /// 初始化 <see cref="MiraiHttpSession"/> 类的新实例
        /// </summary>
        public MiraiHttpSession(IServiceProvider services, IOptions<MiraiHttpSessionOptions> options, IMiraiHttpMessageHandlerInvoker invoker, ChatMessageJsonConverter jsonConverter, HttpClient? client = null)
            : this(services, options.Value, invoker, jsonConverter, client ?? new HttpClient())
        {
           
        }

        /// <summary>
        /// 初始化 <see cref="MiraiHttpSession"/> 类的新实例
        /// </summary>
        protected MiraiHttpSession(IServiceProvider services, MiraiHttpSessionOptions options, IMiraiHttpMessageHandlerInvoker invoker, ChatMessageJsonConverter jsonConverter, HttpClient client)
        {
            _services = services;
            _options = options;
            _client = client;
            _invoker = invoker;
            JsonSerializerOptions chatMessageSerializingOptions = JsonSerializeOptionsFactory.IgnoreNulls;
            chatMessageSerializingOptions.Converters.Add(jsonConverter);
            _chatMessageSerializingOptions = chatMessageSerializingOptions;
            _instanceCts = new CancellationTokenSource();
        }

        public override ISharedMessageChainBuilder GetMessageChainBuilder()
        {
            return new MessageChainBuilder();
        }

        private static void CreateLinkedUserSessionToken(CancellationToken sessionToken,
                                                         CancellationToken userToken,
                                                         out CancellationTokenSource? cts,
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
        public PluginResistration AddPlugin(IMiraiHttpMessageHandler plugin)
        {
            CheckDisposed();
            LinkedList<DynamicHandlerRegistration> registrations = new LinkedList<DynamicHandlerRegistration>();
            IMiraiHttpMessageSubscriptionResolver resolver = _services.GetRequiredService<IMiraiHttpMessageSubscriptionResolver>();
            foreach (IMiraiHttpMessageSubscription? subscription in resolver.ResolveByHandler(plugin.GetType()))
            {
                registrations.AddLast(subscription.AddHandler(plugin));
            }
            return new PluginResistration(registrations);
        }

        /// <inheritdoc/>
        [Obsolete("请调用 AddPlugin 返回的 PluginResistration.Dispose 方法来移除先前注册的插件。预计于 2.2.0 版本移除此方法", true)]
        public void RemovePlugin(IMiraiHttpMessageHandler plugin)
        {
            throw new NotSupportedException("请调用 AddPlugin 返回的 PluginResistration.Dispose 方法来移除先前注册的插件。预计于 2.2.0 版本移除此方法");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeAsyncCore().AsTask().GetAwaiter().GetResult();
            }
        }

        protected async ValueTask DisposeAsyncCore(CancellationTokenSource cts)
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

        /// <summary>
        /// 异步释放当前会话, 并清理相关资源。
        /// </summary>
        /// <remarks>
        /// 本方法线程安全。
        /// </remarks>
        /// <returns>表示此异步操作的 <see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual ValueTask DisposeAsyncCore()
        {
            CancellationTokenSource? previousCts = Volatile.Read(ref _instanceCts);
            if (previousCts != null && Interlocked.CompareExchange(ref _instanceCts, null, previousCts) == previousCts)
            {
                return DisposeAsyncCore(previousCts);
            }
            return default;
        }
    }
}
