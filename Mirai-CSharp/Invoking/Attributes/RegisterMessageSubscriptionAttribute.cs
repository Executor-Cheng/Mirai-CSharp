using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Invoking.Attributes;

namespace Mirai.CSharp.Invoking.Attributes
{
    public class RegisterMiraiMessageSubscriptionAttribute : RegisterMessageSubscriptionAttribute
    {
        public RegisterMiraiMessageSubscriptionAttribute(Type implementationType) : this(null, implementationType)
        {

        }

        public RegisterMiraiMessageSubscriptionAttribute(Type implementationType, ServiceLifetime? lifetime) : this(null, implementationType, lifetime)
        {

        }

        public RegisterMiraiMessageSubscriptionAttribute(Type? serviceType, Type implementationType) : this(serviceType, implementationType, null)
        {

        }

        public RegisterMiraiMessageSubscriptionAttribute(Type? serviceType, Type implementationType, ServiceLifetime? lifetime) : base(serviceType, implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            return typeof(IMiraiMessageSubscription<,>);
        }
    }
}
