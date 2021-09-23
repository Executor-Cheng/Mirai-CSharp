using System;
using System.Collections.Generic;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Invoking;

namespace Mirai.CSharp.HttpApi.Invoking
{
    public interface IMiraiHttpMessageSubscriptionResolver : IMiraiMessageSubscriptionResolver<IMiraiHttpSession, IMiraiHttpMessageSubscription>
    {

    }

    public class MiraiHttpMessageSubscriptionResolver : MiraiMessageSubscriptionResolver<IMiraiHttpSession, IMiraiHttpMessageSubscription>, IMiraiHttpMessageSubscriptionResolver
    {
        public MiraiHttpMessageSubscriptionResolver(IServiceProvider services) : base(services)
        {

        }

        protected override Type GetSubscriptionType(Type messageType)
        {
            return typeof(IMiraiHttpMessageSubscription<>).MakeGenericType(messageType);
        }

        public override IEnumerable<IMiraiHttpMessageSubscription> ResolveByHandler(Type handlerType)
        {
            Type openGeneric = typeof(IMiraiHttpMessageHandler<>);
            List<IMiraiHttpMessageSubscription> subscriptions = new List<IMiraiHttpMessageSubscription>();
            foreach (Type interfaceType in handlerType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    IMiraiHttpMessageSubscription? subscription = ResolveByMessage(interfaceType.GetGenericArguments()[0]);
                    if (subscription != null)
                    {
                        subscriptions.Add(subscription);
                    }
                }
            }
            return subscriptions;
        }
    }
}
