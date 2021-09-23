using System;
using System.Collections.Generic;
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Invoking
{
    public interface IMiraiMessageSubscriptionResolver<in TClient, out TSubscription> : IMessageSubscriptionResolver<TClient, TSubscription> where TClient : IMiraiSession
                                                                                                                                             where TSubscription : IMiraiMessageSubscription
    {

    }

    public class MiraiMessageSubscriptionResolver<TClient, TSubscription> : MessageSubscriptionResolver<TClient, TSubscription>, IMiraiMessageSubscriptionResolver<TClient, TSubscription> where TClient : IMiraiSession
                                                                                                                                                                                           where TSubscription : IMiraiMessageSubscription
    {
        public MiraiMessageSubscriptionResolver(IServiceProvider services) : base(services)
        {
            
        }

        protected override Type GetSubscriptionType(Type messageType)
        {
            return typeof(IMiraiMessageSubscription<,>).MakeGenericType(typeof(TClient), messageType);
        }

        public override IEnumerable<TSubscription> ResolveByHandler(Type handlerType)
        {
            Type openGeneric = typeof(IMiraiMessageHandler<,>);
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
            throw new InvalidOperationException($"给定的 {handlerType.FullName} 不实现 {openGeneric.FullName}");
        }
    }
}
