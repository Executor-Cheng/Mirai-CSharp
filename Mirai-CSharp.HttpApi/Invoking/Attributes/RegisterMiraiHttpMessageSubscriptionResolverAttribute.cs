using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Invoking.Attributes;

namespace Mirai.CSharp.HttpApi.Invoking.Attributes
{
    public class RegisterMiraiHttpMessageSubscriptionResolverAttribute : RegisterMiraiMessageSubscriptionResolverAttribute
    {
        public RegisterMiraiHttpMessageSubscriptionResolverAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        public RegisterMiraiHttpMessageSubscriptionResolverAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            var interfaceType = typeof(IMiraiHttpMessageSubscriptionResolver);
            if (interfaceType.IsAssignableFrom(implementationType))
            {
                return interfaceType;
            }
            throw new ArgumentException($"给定的 {implementationType.Name} 不实现 {interfaceType.Name}", nameof(implementationType));
        }
    }
}
