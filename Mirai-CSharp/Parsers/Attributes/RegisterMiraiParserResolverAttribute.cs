using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Parsers.Attributes;
using Mirai.CSharp.Invoking;

namespace Mirai.CSharp.Parsers.Attributes
{
    /// <summary>
    /// 标记一个 <see cref="MiraiMessageHandlerInvoker{TClientService, TRawdata}"/> 所需要使用的 <see cref="IMiraiMessageParserResolver{TRawdata, TParserService}"/>
    /// </summary>
    public class RegisterMiraiParserResolverAttribute : RegisterParserResolverAttribute
    {
        /// <inheritdoc cref="RegisterMiraiParserResolverAttribute(Type, ServiceLifetime?)"/>
        public RegisterMiraiParserResolverAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        /// <summary>
        /// 使用给定的 <see cref="IMiraiMessageParserResolver{TRawdata, TParserService}"/> 初始化 <see cref="RegisterMiraiParserResolverAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMiraiMessageParserResolver{TRawdata, TMessage}"/> 的实现类类型</param>
        /// <param name="lifetime"><paramref name="implementationType"/> 的生命周期</param>
        public RegisterMiraiParserResolverAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type openGeneric = typeof(IMiraiMessageParserResolver<,>);
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
