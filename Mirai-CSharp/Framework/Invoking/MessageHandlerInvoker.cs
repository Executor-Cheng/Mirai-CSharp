using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Models.General;
using Mirai.CSharp.Framework.Parsers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
#if !NETSTANDARD2_0
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mirai.CSharp.Framework.Invoking
{
    public interface IMessageHandlerInvoker<in TClientService> where TClientService : IMessageClient
    {
        Task HandleMessageAsync<TMessage>(TClientService client, TMessage message) where TMessage : IMessage;
    }

    public interface IMessageHandlerInvoker<in TClientService, TRawdata> : IMessageHandlerInvoker<TClientService> where TClientService : IMessageClient
    {
        Task HandleRawdataAsync(TClientService client, TRawdata rawdata);
    }

    public abstract class MessageHandlerInvoker<TClientService> : IMessageHandlerInvoker<TClientService> where TClientService : IMessageClient
    {
        protected readonly IServiceProvider _Services;

        protected readonly ILogger<MessageHandlerInvoker<TClientService>> _Logger;

        protected readonly IMessageSubscriptionResolver<TClientService, IMessageSubscription> _subscriptionResolver;

        protected virtual IMessageSubscriptionResolver<TClientService, IMessageSubscription> SubscriptionResolver => _subscriptionResolver;

        protected MessageHandlerInvoker(IServiceProvider services,
                                        ILogger<MessageHandlerInvoker<TClientService>> logger,
                                        IMessageSubscriptionResolver<TClientService, IMessageSubscription> subscriptionResolver)
        {
            _Services = services;
            _Logger = logger;
            _subscriptionResolver = subscriptionResolver;
        }

        public virtual Task HandleMessageAsync<TMessage>(TClientService client, TMessage message) where TMessage : IMessage
        {
            if (TryResolveSubscription(out IMessageSubscription<TClientService, TMessage>? subscription))
            {
                return subscription!.HandleMessageAsync(client, message);
            }
            return Task.CompletedTask;
        }

#if NETSTANDARD2_0
        protected virtual bool TryResolveSubscription<TMessage>(out IMessageSubscription<TClientService, TMessage>? subscription) where TMessage : IMessage
#else
        protected virtual bool TryResolveSubscription<TMessage>([NotNullWhen(true)]out IMessageSubscription<TClientService, TMessage>? subscription) where TMessage : IMessage
#endif
        {
#if NETCOREAPP3_0 || NETCOREAPP3_1
            subscription = null;
#else
            Unsafe.SkipInit(out subscription);
#endif
            ref IMessageSubscription? s = ref Unsafe.As<IMessageSubscription<TClientService, TMessage>?, IMessageSubscription?>(ref subscription);
            return TryResolveSubscription(typeof(TMessage), out s);
        }

#if NETSTANDARD2_0
        protected abstract bool TryResolveSubscription(Type messageType, out IMessageSubscription? subscription);
#else
        protected abstract bool TryResolveSubscription(Type messageType, [NotNullWhen(true)]out IMessageSubscription? subscription);
#endif
    }

    public abstract class MessageHandlerInvoker<TClientService, TRawdata> : MessageHandlerInvoker<TClientService>, IMessageHandlerInvoker<TClientService, TRawdata> where TClientService : IMessageClient
    {
        protected readonly ConcurrentDictionary<Type, Type> _SubscriptionMapping = new();

        protected readonly IMessageParserResolver<TRawdata, IMessageParser<TRawdata>> _parserResolver;

        protected virtual IMessageParserResolver<TRawdata, IMessageParser<TRawdata>> ParserResolver => _parserResolver;

        protected MessageHandlerInvoker(IServiceProvider services,
                                        ILogger<MessageHandlerInvoker<TClientService, TRawdata>> logger,
                                        IMessageSubscriptionResolver<TClientService, IMessageSubscription> subscriptionResolver,
                                        IMessageParserResolver<TRawdata, IMessageParser<TRawdata>> parserResolver) : base(services, logger, subscriptionResolver)
        {
            _parserResolver = parserResolver;
        }

        public virtual async Task HandleRawdataAsync(TClientService client, TRawdata rawdata)
        {
            if (TryResolveParsers(in rawdata, out var parsers))
            {
                foreach (var parser in parsers!)
                {
                    if (parser.CanParse(in rawdata))
                    {
                        IMessage<TRawdata> message = parser.Parse(in rawdata);
                        if (TryResolveSubscription(parser.MessageType, out IMessageSubscription? subscription))
                        {
                            await subscription!.HandleMessageAsync(client, message).ConfigureAwait(false);
                        }
                    }
                }
            }
        }

#if NETSTANDARD2_0
        protected abstract bool TryResolveParsers(in TRawdata rawdata, out IEnumerable<IMessageParser<TRawdata>>? parsers);
#else
        protected abstract bool TryResolveParsers(in TRawdata rawdata, [NotNullWhen(true)]out IEnumerable<IMessageParser<TRawdata>>? parsers);
#endif
    }
}
