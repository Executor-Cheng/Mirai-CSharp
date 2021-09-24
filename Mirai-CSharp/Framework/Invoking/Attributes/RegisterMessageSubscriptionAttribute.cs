using System;
using Mirai.CSharp.Framework.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Mirai.CSharp.Framework.Invoking.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RegisterMessageSubscriptionAttribute : RegisterBaseAttribute
    {
        public RegisterMessageSubscriptionAttribute(Type implementationType) : this(null, implementationType)
        {

        }

        public RegisterMessageSubscriptionAttribute(Type implementationType, ServiceLifetime? lifetime) : this(null, implementationType, lifetime)
        {

        }

        public RegisterMessageSubscriptionAttribute(Type? serviceType, Type implementationType) : this(serviceType, implementationType, null)
        {

        }

        public RegisterMessageSubscriptionAttribute(Type? serviceType, Type implementationType, ServiceLifetime? lifetime) : base(serviceType, implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            return typeof(IMessageSubscription);
        }
    }
}
