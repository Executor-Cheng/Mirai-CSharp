using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Invoking.Attributes;

namespace Mirai.CSharp.HttpApi.Invoking.Attributes
{
    public class RegisterMiraiHttpMessageSubscriptionAttribute : RegisterMiraiMessageSubscriptionAttribute
    {
        public RegisterMiraiHttpMessageSubscriptionAttribute(Type implementationType) : base(implementationType)
        {

        }

        public RegisterMiraiHttpMessageSubscriptionAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type serviceType = typeof(IMiraiHttpMessageSubscription),
                 genericServiceType = typeof(IMiraiHttpMessageSubscription<>);
            var interfaces = implementationType.GetInterfaces();
            if (implementationType.IsGenericTypeDefinition) // impl IMiraiHttpMessageSubscription<> or IMiraiHttpMessageSubscription<xxxxx>
            {
                if (interfaces.Any(p => p == genericServiceType || p.IsGenericType && p.GetGenericTypeDefinition() == genericServiceType))
                {
                    return genericServiceType;
                }
                throw new InvalidOperationException($"{implementationType} 未实现 {genericServiceType}");
            }
            var constructedServiceType = interfaces.FirstOrDefault(p => p.IsGenericType && p.GetGenericTypeDefinition() == genericServiceType);
            if (constructedServiceType != null)
            {
                return constructedServiceType;
            }
            if (serviceType.IsAssignableFrom(implementationType))
            {
                return serviceType;
            }
            throw new InvalidOperationException($"{implementationType} 未实现 {genericServiceType} 或 {serviceType}");
        }
    }
}
