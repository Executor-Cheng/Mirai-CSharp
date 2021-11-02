using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.Framework.Invoking.Attributes;
using Mirai.CSharp.Framework.Parsers;
using Mirai.CSharp.Framework.Parsers.Attributes;

namespace Mirai.CSharp.Framework.Builders
{
    public interface IMessageFrameworkBuilder<in TInvokerService, in TClientService, in THandlerService>
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
        where THandlerService : class, IMessageHandler
    {
        IServiceCollection Services { get; }

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>() where THandler : class, THandlerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(ServiceLifetime lifetime) where THandler : class, THandlerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler(THandlerService handlerInstance);

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory) where THandler : class, THandlerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory, ServiceLifetime lifetime) where THandler : class, THandlerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>() where TInvoker : class, TInvokerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(ServiceLifetime lifetime) where TInvoker : class, TInvokerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker(TInvokerService invokerInstance);

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory) where TInvoker : class, TInvokerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory, ServiceLifetime lifetime) where TInvoker : class, TInvokerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>() where TClient : class, TClientService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(ServiceLifetime lifetime) where TClient : class, TClientService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient(TClientService clientInstance);

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory) where TClient : class, TClientService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory, ServiceLifetime lifetime) where TClient : class, TClientService;
    }

    public abstract class MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> : IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService>
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
        where THandlerService : class, IMessageHandler
    {
        protected virtual ServiceLifetime DefaultHandlerLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultInvokerLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultClientLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultSubscriptionLifetime => ServiceLifetime.Scoped;

        protected virtual ServiceLifetime DefaultSubscriptionResolverLifetime => DefaultSubscriptionLifetime;

        public IServiceCollection Services { get; }

        public MessageFrameworkBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

#if NETSTANDARD2_0
        protected bool TryAddService(Type serviceType, Type implementationType, ServiceLifetime lifetime, out ServiceDescriptor? addedDescriptor)
#else
        protected bool TryAddService(Type serviceType, Type implementationType, ServiceLifetime lifetime, [NotNullWhen(true)] out ServiceDescriptor? addedDescriptor)
#endif
        {
            ServiceDescriptor descriptor = new(serviceType, implementationType, lifetime);
            return TryAddService(descriptor, out addedDescriptor);
        }

#if NETSTANDARD2_0
        protected bool TryAddService(Type serviceType, object implementationInstance, out ServiceDescriptor? addedDescriptor)
#else
        protected bool TryAddService(Type serviceType, object implementationInstance, [NotNullWhen(true)]out ServiceDescriptor? addedDescriptor)
#endif
        {
            ServiceDescriptor descriptor = new(serviceType, implementationInstance);
            return TryAddService(descriptor, out addedDescriptor);
        }

#if NETSTANDARD2_0
        protected bool TryAddService(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime, out ServiceDescriptor? addedDescriptor)
#else
        protected bool TryAddService(Type serviceType, Func<IServiceProvider, object> factory, ServiceLifetime lifetime, [NotNullWhen(true)]out ServiceDescriptor? addedDescriptor)
#endif
        {
            ServiceDescriptor descriptor = new(serviceType, factory, lifetime);
            return TryAddService(descriptor, out addedDescriptor);
        }

#if NETSTANDARD2_0
        protected bool TryAddService(ServiceDescriptor descriptor, out ServiceDescriptor? addedDescriptor)
#else
        protected bool TryAddService(ServiceDescriptor descriptor, [NotNullWhen(true)]out ServiceDescriptor? addedDescriptor)
#endif
        {
            int count = Services.Count;
            Services.TryAddEnumerable(descriptor);
            if (count != Services.Count)
            {
                addedDescriptor = descriptor;
                return true;
            }
            addedDescriptor = null;
            return false;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>() where THandler : class, THandlerService
        {
            return this.AddHandler<THandler>(DefaultHandlerLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(ServiceLifetime lifetime) where THandler : class, THandlerService
        {
            TryAddService(typeof(THandlerService), typeof(THandler), lifetime, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler(THandlerService handlerInstance)
        {
            TryAddService(typeof(THandlerService), handlerInstance, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory) where THandler : class, THandlerService
        {
            return this.AddHandler(factory, DefaultHandlerLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory, ServiceLifetime lifetime) where THandler : class, THandlerService
        {
            TryAddService(typeof(THandlerService), factory, lifetime, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>() where TInvoker : class, TInvokerService
        {
            return this.AddInvoker<TInvoker>(DefaultInvokerLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(ServiceLifetime lifetime) where TInvoker : class, TInvokerService
        {
            Type invokerType = typeof(TInvoker);
            TryAddService(typeof(TInvokerService), invokerType, lifetime, out _);
            ResolveMessageSubscription(invokerType, lifetime);
            ResolveMessageSubscriptionResolver(invokerType, lifetime);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker(TInvokerService invokerInstance)
        {
            Type instanceType = typeof(TInvokerService);
            TryAddService(typeof(TInvokerService), invokerInstance, out _);
            ResolveMessageSubscription(instanceType, ServiceLifetime.Singleton);
            ResolveMessageSubscriptionResolver(instanceType, ServiceLifetime.Singleton);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory) where TInvoker : class, TInvokerService
        {
            return this.AddInvoker(factory, DefaultInvokerLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory, ServiceLifetime lifetime) where TInvoker : class, TInvokerService
        {
            Type invokerType = typeof(TInvokerService);
            TryAddService(invokerType, factory, lifetime, out _);
            ResolveMessageSubscription(invokerType, lifetime);
            ResolveMessageSubscriptionResolver(invokerType, lifetime);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>() where TClient : class, TClientService
        {
            return this.AddClient<TClient>(DefaultClientLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(ServiceLifetime lifetime) where TClient : class, TClientService
        {
            TryAddService(typeof(TClientService), typeof(TClient), lifetime, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient(TClientService clientInstance)
        {
            TryAddService(typeof(TClientService), clientInstance, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory) where TClient : class, TClientService
        {
            return this.AddClient(factory, DefaultClientLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddClient<TClient>(Func<IServiceProvider, TClient> factory, ServiceLifetime lifetime) where TClient : class, TClientService
        {
            TryAddService(typeof(TClientService), factory, lifetime, out _);
            return this;
        }

        protected virtual void ResolveMessageSubscription(Type invokerType, ServiceLifetime? lifetime)
        {
            IEnumerable<RegisterMessageSubscriptionAttribute> attributes = invokerType.GetCustomAttributes<RegisterMessageSubscriptionAttribute>(false);
            foreach (RegisterMessageSubscriptionAttribute attribute in attributes)
            {
                Services.Add(new ServiceDescriptor(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultSubscriptionLifetime));
            }
        }

        protected virtual void ResolveMessageSubscriptionResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            IEnumerable<RegisterMessageSubscriptionResolverAttribute> attributes = invokerType.GetCustomAttributes<RegisterMessageSubscriptionResolverAttribute>(false);
            foreach (RegisterMessageSubscriptionResolverAttribute attribute in attributes)
            {
                Services.Add(new ServiceDescriptor(attribute.ServiceType, attribute.ImplementationType, lifetime ?? attribute.Lifetime ?? DefaultSubscriptionResolverLifetime));
            }
        }

        protected virtual void ReplaceServiceLifetime(ICollection<ServiceDescriptor> addedDescriptors, ServiceLifetime lifetime)
        {
            for (int i = Services.Count - 1; i >= 0; i--)
            {
                ServiceDescriptor descriptor = Services[i];
                if (addedDescriptors.Contains(descriptor) && descriptor.ImplementationInstance == null)
                {
                    Services.RemoveAt(i);
                    addedDescriptors.Remove(descriptor);
                    if (descriptor.ImplementationFactory != null)
                    {
                        descriptor = new ServiceDescriptor(descriptor.ServiceType, descriptor.ImplementationFactory, lifetime);
                    }
                    else
                    {
                        descriptor = new ServiceDescriptor(descriptor.ServiceType, descriptor.ImplementationType!, lifetime);
                    }
                    Services.Add(descriptor);
                    addedDescriptors.Add(descriptor);
                }
            }
        }
    }

    public interface IMessageFrameworkBuilder<in TInvokerService, in TClientService, in THandlerService, in TParserService> : IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService>
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
        where THandlerService : class, IMessageHandler
        where TParserService : class, IMessageParser
    {
        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>() where TParser : class, TParserService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(ServiceLifetime lifetime) where TParser : class, TParserService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser(TParserService parserInstance);

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory) where TParser : class, TParserService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory, ServiceLifetime lifetime) where TParser : class, TParserService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> ResolveParser<THandler>(ServiceLifetime? lifetime = null) where THandler : class, THandlerService;

        IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> ResolveParser(Type handlerType, ServiceLifetime? lifetime = null);
    }

    public abstract class MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> : MessageFrameworkBuilder<TInvokerService, TClientService, THandlerService>, IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService>
        where TParserService : class, IMessageParser
        where THandlerService : class, IMessageHandler
        where TInvokerService : class, IMessageHandlerInvoker<TClientService>
        where TClientService : class, IMessageClient
    {
        protected virtual ServiceLifetime DefaultParserLifetime => ServiceLifetime.Singleton;

        protected virtual ServiceLifetime DefaultParserResolverLifetime => DefaultParserLifetime;

        protected ServiceLifetime _maxParserLifetime;

        protected ServiceLifetime _maxParserResolverLifetime;

        protected HashSet<ServiceDescriptor> _addedParserResolvers;

        public MessageFrameworkBuilder(IServiceCollection services) : base(services)
        {
            _addedParserResolvers = new HashSet<ServiceDescriptor>();
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>() where TParser : class, TParserService
        {
            return this.AddParser<TParser>(DefaultParserLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(ServiceLifetime lifetime) where TParser : class, TParserService
        {
            TryAddService(typeof(TParserService), typeof(TParser), lifetime, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser(TParserService parserInstance)
        {
            TryAddService(typeof(TParserService), parserInstance, out _);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory) where TParser : class, TParserService
        {
            return this.AddParser(factory, DefaultParserLifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> AddParser<TParser>(Func<IServiceProvider, TParser> factory, ServiceLifetime lifetime) where TParser : class, TParserService
        {
            TryAddService(typeof(TParserService), factory, lifetime, out _);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(ServiceLifetime lifetime)
        {
            base.AddHandler<THandler>(lifetime);
            ResolveMessageParser(typeof(THandler), lifetime);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler(THandlerService handlerInstance)
        {
            base.AddHandler(handlerInstance);
            ResolveMessageParser(handlerInstance.GetType(), ServiceLifetime.Singleton);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddHandler<THandler>(Func<IServiceProvider, THandler> factory, ServiceLifetime lifetime)
        {
            base.AddHandler(factory, lifetime);
            ResolveMessageParser(typeof(THandler), lifetime);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(ServiceLifetime lifetime)
        {
            base.AddInvoker<TInvoker>(lifetime);
            ResolveMessageParserResolver(typeof(TInvoker), _maxParserLifetime);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker(TInvokerService invokerInstance)
        {
            base.AddInvoker(invokerInstance);
            ResolveMessageParserResolver(invokerInstance.GetType(), ServiceLifetime.Singleton);
            return this;
        }

        public override IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService> AddInvoker<TInvoker>(Func<IServiceProvider, TInvoker> factory, ServiceLifetime lifetime)
        {
            base.AddInvoker(factory, lifetime);
            ResolveMessageParserResolver(typeof(TInvoker), _maxParserLifetime);
            return this;
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> ResolveParser<THandler>(ServiceLifetime? lifetime = null) where THandler : class, THandlerService
        {
            return ResolveParser(typeof(THandler), lifetime);
        }

        public virtual IMessageFrameworkBuilder<TInvokerService, TClientService, THandlerService, TParserService> ResolveParser(Type handlerType, ServiceLifetime? lifetime = null)
        {
            ResolveMessageParser(handlerType, lifetime);
            return this;
        }

        protected virtual void ResolveMessageParser(Type handlerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterParserAttribute attribute in handlerType.GetCustomAttributes<RegisterParserAttribute>(false))
            {
                ServiceLifetime parserLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserLifetime;
                AddMessageParser(attribute.ServiceType, attribute.ImplementationType, parserLifetime);
            }
        }

        protected virtual void ResolveMessageParserResolver(Type invokerType, ServiceLifetime? lifetime)
        {
            foreach (RegisterParserResolverAttribute attribute in invokerType.GetCustomAttributes<RegisterParserResolverAttribute>(false))
            {
                ServiceLifetime parserResolverLifetime = lifetime ?? attribute.Lifetime ?? DefaultParserResolverLifetime;
                AddMessageParserResolver(attribute.ServiceType, attribute.ImplementationType, parserResolverLifetime);
            }
        }

        protected void AddMessageParser(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            if (TryAddService(serviceType, implementationType, lifetime, out _))
            {
                if (lifetime > _maxParserLifetime)
                {
                    _maxParserLifetime = lifetime;
                    if (lifetime > _maxParserResolverLifetime)
                    {
                        _maxParserResolverLifetime = lifetime;
                        ReplaceServiceLifetime(_addedParserResolvers, lifetime);
                    }
                }
            }
        }

        protected void AddMessageParserResolver(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            if (TryAddService(serviceType, implementationType, lifetime, out ServiceDescriptor? addedDescriptor))
            {
                if (lifetime > _maxParserResolverLifetime)
                {
                    _maxParserResolverLifetime = lifetime;
                }
                _addedParserResolvers.Add(addedDescriptor!);
            }
        }
    }
}
