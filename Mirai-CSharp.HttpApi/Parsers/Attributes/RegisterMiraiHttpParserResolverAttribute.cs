using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.Parsers.Attributes;

namespace Mirai.CSharp.HttpApi.Parsers.Attributes
{
    /// <summary>
    /// 标记一个 <see cref="MiraiHttpMessageHandlerInvoker"/> 所需要使用的 <see cref="IMiraiHttpMessageParserResolver"/>
    /// </summary>
    public class RegisterMiraiHttpMessageParserResolverAttribute
        : RegisterMiraiParserResolverAttribute
    {
        /// <inheritdoc cref="RegisterMiraiHttpMessageParserResolverAttribute(Type, ServiceLifetime?)"/>
        public RegisterMiraiHttpMessageParserResolverAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        /// <summary>
        /// 使用给定的 <paramref name="implementationType"/> 初始化 <see cref="RegisterMiraiHttpParserAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMiraiHttpMessageParserResolver"/> 的实现类类型</param>
        /// <param name="lifetime"><paramref name="implementationType"/> 的生命周期</param>
        public RegisterMiraiHttpMessageParserResolverAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            var interfaceType = typeof(IMiraiHttpMessageParserResolver);
            if (interfaceType.IsAssignableFrom(implementationType))
            {
                return interfaceType;
            }
            throw new ArgumentException($"给定的 {implementationType.Name} 不实现 {interfaceType.Name}", nameof(implementationType));
        }
    }
}
