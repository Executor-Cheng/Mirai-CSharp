using System;
using Microsoft.Extensions.DependencyInjection;

namespace Mirai.CSharp.Framework.Attributes
{
    public abstract class RegisterBaseAttribute : Attribute
    {
        public Type ServiceType { get; }

        public Type ImplementationType { get; }

        public ServiceLifetime? Lifetime { get; }

        protected RegisterBaseAttribute(Type implementationType) : this(null, implementationType)
        {

        }

        protected RegisterBaseAttribute(Type implementationType, ServiceLifetime? lifetime) : this(null, implementationType, lifetime)
        {

        }

        protected RegisterBaseAttribute(Type? serviceType, Type implementationType)
        {
            ServiceType = serviceType ?? GetServiceType(implementationType);
            ImplementationType = implementationType;
        }

        protected RegisterBaseAttribute(Type? serviceType, Type implementationType, ServiceLifetime? lifetime) : this(serviceType, implementationType)
        {
            Lifetime = lifetime;
        }

        protected virtual Type GetServiceType(Type implementationType)
        {
            throw new NotSupportedException();
        }
    }
}
