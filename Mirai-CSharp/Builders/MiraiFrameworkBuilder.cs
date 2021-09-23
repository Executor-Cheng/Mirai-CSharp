using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Builders;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Invoking;
using Mirai.CSharp.Invoking.Attributes;
using Mirai.CSharp.Parsers;
using Mirai.CSharp.Parsers.Attributes;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Builders
{
    public interface IMiraiFrameworkBuilder<in TInvokerService, in TClientService, in THandlerService, in TParserService> : IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService>
        where TParserService : class, IMiraiMessageParser
        where THandlerService : class, IMiraiMessageHandler
        where TInvokerService : class, IMiraiMessageHandlerInvoker<TClientService>
        where TClientService : class, IMiraiSession
    {

    }

    public class MiraiFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> : MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService>,
                                                                                                           IMiraiFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService>
        where TParserService : class, IMiraiMessageParser
        where THandlerService : class, IMiraiMessageHandler
        where TInvokerService : class, IMiraiMessageHandlerInvoker<TClientService>
        where TClientService : class, IMiraiSession
    {
        public MiraiFrameworkBuilder(IServiceCollection services) : base(services)
        {

        }

        protected override void ResolveMessageParser(Type handlerType, ServiceLifetime? lifetime)
        {
            foreach (var attribute in handlerType.GetCustomAttributes<RegisterMiraiParserAttribute>(false))
            {
                ServiceLifetime parserLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserLifetime;
                AddMessageParser(attribute.ServiceType, attribute.ImplementationType, parserLifetime);
            }
        }

        protected override void ResolveMessageSubscription(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (var attribute in invokerType.GetCustomAttributes<RegisterMiraiMessageSubscriptionAttribute>(false))
            {
                TryAddService(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultSubscriptionLifetime, out _);
            }
        }

        protected override void ResolveMessageParserResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiParserResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiParserResolverAttribute>(false))
            {
                ServiceLifetime parserResolverLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime;
                AddMessageParserResolver(attribute.ServiceType, attribute.ImplementationType, parserResolverLifetime);
            }
        }

        protected override void ResolveMessageSubscriptionResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterMiraiMessageSubscriptionResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterMiraiMessageSubscriptionResolverAttribute>(false))
            {
                TryAddService(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime, out _);
            }
        }
    }

    public static class MiraiFrameworkServiceCollectionExtensions
    {
        public static MiraiFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddMiraiFramework<TInvokerService, TClientService, THandlerService, TParserService>(this IServiceCollection services) where TParserService : class, IMiraiMessageParser
                                                                                                                                                                                                                                    where THandlerService : class, IMiraiMessageHandler
                                                                                                                                                                                                                                    where TInvokerService : class, IMiraiMessageHandlerInvoker<TClientService>
                                                                                                                                                                                                                                    where TClientService : class, IMiraiSession
        {
            return new MiraiFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService>(services);
        }

        public static MiraiBaseFrameworkBuilder AddMiraiBaseFramework(this IServiceCollection services)
        {
            return new MiraiBaseFrameworkBuilder(services);
        }
    }
}
