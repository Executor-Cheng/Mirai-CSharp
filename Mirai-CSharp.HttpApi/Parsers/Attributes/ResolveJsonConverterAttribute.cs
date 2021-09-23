using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Attributes;

namespace Mirai.CSharp.HttpApi.Parsers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class ResolveJsonConverterAttribute : RegisterBaseAttribute
    {
        public ResolveJsonConverterAttribute(Type implementationType) : this(implementationType, default(ServiceLifetime?))
        {
            
        }

        public ResolveJsonConverterAttribute(Type implementationType, ServiceLifetime? lifetime) : this(implementationType, implementationType, lifetime)
        {

        }

        public ResolveJsonConverterAttribute(Type? serviceType, Type implementationType) : this(serviceType, implementationType, null)
        {

        }

        public ResolveJsonConverterAttribute(Type? serviceType, Type implementationType, ServiceLifetime? lifetime) : base(serviceType, implementationType, lifetime)
        {
            Type? baseType = serviceType?.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(JsonConverter<>))
                {
                    return;
                }
            }
            throw new ArgumentException($"给定的 {serviceType} 不继承 JsonConverter<>");
        }
    }
}
