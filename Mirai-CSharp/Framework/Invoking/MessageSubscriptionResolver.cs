using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Handlers;

namespace Mirai.CSharp.Framework.Invoking
{
    public interface IMessageSubscriptionResolver<in TClient, out TSubscription> where TClient : IMessageClient
                                                                                 where TSubscription : IMessageSubscription
    {
        IEnumerable<TSubscription> ResolveByHandler(Type handlerType);

        TSubscription? ResolveByMessage(Type messageType);
    }

    public class MessageSubscriptionResolver<TClient, TSubscription> : IMessageSubscriptionResolver<TClient, TSubscription> where TClient : IMessageClient
                                                                                                                            where TSubscription : IMessageSubscription
    {
        protected readonly IServiceProvider _services;

        protected readonly ConcurrentDictionary<Type, Type> _subscriptionTypeMapping = new();

        public MessageSubscriptionResolver(IServiceProvider services)
        {
            _services = services;
        }

        protected virtual Type GetSubscriptionType(Type messageType)
        {
            return typeof(IMessageSubscription<,>).MakeGenericType(typeof(TClient), messageType);
        }

        public virtual IEnumerable<TSubscription> ResolveByHandler(Type handlerType)
        {
            Type openGeneric = typeof(IMessageHandler<,>);
            List<TSubscription> subscriptions = new List<TSubscription>();
            foreach (Type interfaceType in handlerType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    Type[] genericArguments = interfaceType.GetGenericArguments();
                    Type clientType = genericArguments[0];
                    Type thisClientType = typeof(TClient);
                    if (thisClientType.IsAssignableFrom(genericArguments[0]))
                    {
                        TSubscription? subscription = ResolveByMessage(genericArguments[1]);
                        if (subscription != null)
                        {
                            subscriptions.Add(subscription);
                        }
                        continue;
                    }
                    throw new InvalidOperationException($"给定的 {handlerType.FullName} 标定的客户端类型 {clientType.FullName} 和 {thisClientType.FullName} 不兼容");
                }
            }
            return subscriptions;
        }

        public virtual TSubscription? ResolveByMessage(Type messageType)
        {
            if (!_subscriptionTypeMapping.TryGetValue(messageType, out Type? subscriptionType))
            {
                _subscriptionTypeMapping[messageType] = subscriptionType = GetSubscriptionType(messageType);
            }
            return (TSubscription?)_services.GetService(subscriptionType);
        }
    }
}
