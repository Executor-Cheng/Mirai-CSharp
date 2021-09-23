using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Attributes;
using Mirai.CSharp.Framework.Invoking;

namespace Mirai.CSharp.Framework.Parsers.Attributes
{
    /// <summary>
    /// 标记一个 <see cref="MessageHandlerInvoker{TClientService, TRawdata}"/> 所需要使用的 <see cref="IMessageParserResolver{TRawdata, TParserService}"/>
    /// </summary>
    public class RegisterParserResolverAttribute : RegisterBaseAttribute
    {
        /// <inheritdoc cref="RegisterParserResolverAttribute(Type, ServiceLifetime?)"/>
        public RegisterParserResolverAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        /// <summary>
        /// 使用给定的 <see cref="IMessageParserResolver{TRawdata, TParserService}"/> 初始化 <see cref="RegisterParserResolverAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMessageParser{TRawdata, TMessage}"/> 的实现类类型</param>
        /// <param name="lifetime"><paramref name="implementationType"/> 的生命周期</param>
        public RegisterParserResolverAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type openGeneric = typeof(IMessageParserResolver<,>);
            foreach (Type interfaceType in implementationType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    return interfaceType;
                }
            }
            throw new ArgumentException($"给定的 {implementationType.FullName} 不实现 {openGeneric.FullName}", nameof(implementationType));
        }
    }
}
