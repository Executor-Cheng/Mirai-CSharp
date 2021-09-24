using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Parsers.Attributes;

namespace Mirai.CSharp.Parsers.Attributes
{
    /// <summary>
    /// 标记一个消息类、消息接口或者消息处理类所需要使用的 <see cref="IMiraiMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    public class RegisterMiraiParserAttribute : RegisterParserAttribute
    {
        /// <inheritdoc cref="RegisterMiraiParserAttribute(Type, ServiceLifetime?)"/>
        public RegisterMiraiParserAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        /// <summary>
        /// 使用给定的 <see cref="IMiraiMessageParser{TRawdata, TMessage}"/> 初始化 <see cref="RegisterMiraiParserAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMiraiMessageParser{TRawdata, TMessage}"/> 的实现类类型</param>
        /// <param name="lifetime"><paramref name="implementationType"/> 的生命周期</param>
        public RegisterMiraiParserAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type openGeneric = typeof(IMiraiMessageParser<,>);
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
