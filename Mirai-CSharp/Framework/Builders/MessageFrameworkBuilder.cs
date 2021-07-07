using System;
using System.Collections.Generic;
using System.Reflection;
using Mirai_CSharp.Framework.Clients;
using Mirai_CSharp.Framework.Handlers;
using Mirai_CSharp.Framework.Invoking;
using Mirai_CSharp.Framework.Invoking.Attributes;
using Mirai_CSharp.Framework.Parsers;
using Mirai_CSharp.Framework.Parsers.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Mirai_CSharp.Framework.Builders
{
    public abstract class MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService>
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
        where THandlerService : class, IMessageHandler
    {
        protected virtual ServiceLifetime DefaultHandlerLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultInvokerLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultClientLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultSubscriptionLifetime => ServiceLifetime.Scoped;

        public IServiceCollection Services { get; }

        public MessageFrameworkBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected void AddService(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceDescriptor descriptor = new(serviceType, implementationType, lifetime);
            AddService(descriptor);
        }

        protected void AddService(Type serviceType, object implementationInstance)
        {
            ServiceDescriptor descriptor = new(serviceType, implementationInstance);
            AddService(descriptor);
        }

        protected void AddService(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime)
        {
            ServiceDescriptor descriptor = new(serviceType, factory, lifetime);
            AddService(descriptor);
        }

        protected void AddService(ServiceDescriptor descriptor)
        {
            for (int i = 0; i < Services.Count; i++)
            {
                ServiceDescriptor r = Services[i];
                if (r.ServiceType == descriptor.ServiceType &&
                    r.ImplementationType == descriptor.ImplementationType)
                {
                    return;
                }
            }
            Services.Add(descriptor);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>() where THandler : class, THandlerService
        {
            return this.AddHandler<THandler>(DefaultHandlerLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(ServiceLifetime lifetime) where THandler : class, THandlerService
        {
            AddService(typeof(THandlerService), typeof(THandler), lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler(THandlerService handlerInstance)
        {
            AddService(typeof(THandlerService), handlerInstance);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory) where THandler : class, THandlerService
        {
            return this.AddHandler(factory, DefaultHandlerLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory, ServiceLifetime lifetime) where THandler : class, THandlerService
        {
            AddService(typeof(THandlerService), factory, lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>() where TInvoker : class, TInvokerService
        {
            return this.AddInvoker<TInvoker>(DefaultInvokerLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(ServiceLifetime lifetime) where TInvoker : class, TInvokerService
        {
            Type invokerType = typeof(TInvoker);
            AddService(typeof(TInvokerService), invokerType, lifetime);
            ResolveMessageSubscription(invokerType, lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker(TInvokerService invokerInstance)
        {
            AddService(typeof(TInvokerService), invokerInstance);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory) where TInvoker : class, TInvokerService
        {
            return this.AddInvoker(factory, DefaultInvokerLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory, ServiceLifetime lifetime) where TInvoker : class, TInvokerService
        {
            AddService(typeof(TInvokerService), factory, lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>() where TClient : class, TClientService
        {
            return this.AddClient<TClient>(DefaultClientLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(ServiceLifetime lifetime) where TClient : class, TClientService
        {
            AddService(typeof(TClientService), typeof(TClient), lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient(TClientService clientInstance)
        {
            AddService(typeof(TClientService), clientInstance);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory) where TClient : class, TClientService
        {
            return this.AddClient(factory, DefaultClientLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory, ServiceLifetime lifetime) where TClient : class, TClientService
        {
            AddService(typeof(TClientService), factory, lifetime);
            return this;
        }

        protected virtual void ResolveMessageSubscription(Type invokerType, ServiceLifetime? lifetime)
        {
            IEnumerable<RegisterMessageSubscriptionAttribute> attributes = invokerType.GetCustomAttributes<RegisterMessageSubscriptionAttribute>();
            foreach (RegisterMessageSubscriptionAttribute attribute in attributes)
            {
                Services.Add(new ServiceDescriptor(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultSubscriptionLifetime));
            }
        }
    }

    public abstract class MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> : MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService>
        where TParserService : class, IMessageParser<TRawdata>
        where THandlerService : class, IMessageHandler
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
    {
        protected virtual ServiceLifetime DefaultParserLifetime => ServiceLifetime.Singleton;

        public MessageFrameworkBuilder(IServiceCollection services) : base(services)
        {

        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> AddParser<TParser>() where TParser : class, TParserService
        {
            return this.AddParser<TParser>(DefaultParserLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> AddParser<TParser>(ServiceLifetime lifetime) where TParser : class, TParserService
        {
            AddService(typeof(TParserService), typeof(TParser), lifetime);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> AddParser(TParserService parserInstance)
        {
            AddService(typeof(TParserService), parserInstance);
            return this;
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory) where TParser : class, TParserService
        {
            return this.AddParser(factory, DefaultParserLifetime);
        }

        public virtual MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TRawdata, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory, ServiceLifetime lifetime) where TParser : class, TParserService
        {
            AddService(typeof(TParserService), factory, lifetime);
            return this;
        }

        public override MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(ServiceLifetime lifetime)
        {
            base.AddHandler<THandler>(lifetime);
            ResolveMessageParser(typeof(THandler), lifetime);
            return this;
        }

        public override MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler(THandlerService handlerInstance)
        {
            base.AddHandler(handlerInstance);
            ResolveMessageParser(handlerInstance.GetType(), ServiceLifetime.Singleton);
            return this;
        }

        public override MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory, ServiceLifetime lifetime)
        {
            base.AddHandler(factory, lifetime);
            ResolveMessageParser(typeof(THandler), lifetime);
            return this;
        }

        protected virtual void ResolveMessageParser(Type handlerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterParserAttribute attribute in handlerType.GetCustomAttributes<RegisterParserAttribute>())
            {
                AddService(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultParserLifetime);
            }
        }
    }
}
