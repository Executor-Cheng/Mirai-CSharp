using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Invoking.Attributes;

namespace Mirai.CSharp.Invoking.Attributes
{
    public class RegisterMiraiMessageSubscriptionResolverAttribute : RegisterMessageSubscriptionResolverAttribute
    {
        public RegisterMiraiMessageSubscriptionResolverAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        public RegisterMiraiMessageSubscriptionResolverAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type openGeneric = typeof(IMiraiMessageSubscriptionResolver<,>);
            foreach (Type interfaceType in implementationType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    return interfaceType;
                }
            }
            throw new ArgumentException($"给定的 {implementationType.Name} 不实现 {openGeneric.Name}", nameof(implementationType));
        }
    }
}
