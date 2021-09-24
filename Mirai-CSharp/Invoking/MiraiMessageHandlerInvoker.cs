using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.Parsers;
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.Session;
using Mirai.CSharp.Invoking.Attributes;
using Mirai.CSharp.Framework.Parsers;
#if !NETSTANDARD2_0
using System.Diagnostics.CodeAnalysis;
#endif

namespace Mirai.CSharp.Invoking
{
    public interface IMiraiMessageHandlerInvoker<in TClientService> : IMessageHandlerInvoker<TClientService> where TClientService : IMiraiSession
    {
        
    }

    public interface IMiraiMessageHandlerInvoker<in TClientService, TRawdata> : IMiraiMessageHandlerInvoker<TClientService>, IMessageHandlerInvoker<TClientService, TRawdata> where TClientService : IMiraiSession
    {
        
    }

    [RegisterMiraiMessageSubscription(typeof(MiraiMessageSubscription<,>))]
    public class MiraiMessageHandlerInvoker<TClientService, TRawdata> : MessageHandlerInvoker<TClientService, TRawdata>, IMiraiMessageHandlerInvoker<TClientService, TRawdata>
                                                                        where TClientService : IMiraiSession
    {
        public MiraiMessageHandlerInvoker(IServiceProvider services,
                                          ILogger<MiraiMessageHandlerInvoker<TClientService, TRawdata>> logger,
                                          IMiraiMessageParserResolver<TRawdata, IMiraiMessageParser<TRawdata>> parserResolver,
                                          IMiraiMessageSubscriptionResolver<TClientService, IMiraiMessageSubscription> subscriptionResolver) : base(services, logger, subscriptionResolver, parserResolver)
        {
            
        }

#if NETSTANDARD2_0
        protected override bool TryResolveParsers(in TRawdata rawdata, out IEnumerable<IMessageParser<TRawdata>>? parsers)
#else
        protected override bool TryResolveParsers(in TRawdata rawdata, [NotNullWhen(true)]out IEnumerable<IMessageParser<TRawdata>>? parsers)
#endif
        {
            parsers = ParserResolver.ResolveParsers(in rawdata);
            return parsers != null;
        }

#if NETSTANDARD2_0
        protected override bool TryResolveSubscription(Type messageType, out IMessageSubscription? subscription)
#else
        protected override bool TryResolveSubscription(Type messageType, [NotNullWhen(true)]out IMessageSubscription? subscription)
#endif
        {
            return (subscription = SubscriptionResolver.ResolveByMessage(messageType)) != null;
        }
    }
}
