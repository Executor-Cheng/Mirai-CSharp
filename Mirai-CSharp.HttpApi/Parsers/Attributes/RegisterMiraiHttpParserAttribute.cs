using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.Parsers.Attributes;

namespace Mirai.CSharp.HttpApi.Parsers.Attributes
{
    /// <summary>
    /// 标记一个消息类、消息接口、或者消息处理类所需要使用的 <see cref="IMiraiHttpMessageParser{TMessage}"/>
    /// </summary>
    public class RegisterMiraiHttpParserAttribute : RegisterParserAttribute
    {
        /// <inheritdoc cref="RegisterMiraiHttpParserAttribute(Type, ServiceLifetime?)"/>
        public RegisterMiraiHttpParserAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        /// <summary>
        /// 使用给定的 <paramref name="implementationType"/> 初始化 <see cref="RegisterMiraiHttpParserAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMiraiHttpMessageParser{TMessage}"/> 的实现类类型</param>
        /// <param name="lifetime"><paramref name="implementationType"/> 的生命周期</param>
        public RegisterMiraiHttpParserAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            var openGeneric = typeof(IMiraiHttpMessageParser<>);
            foreach (var interfaceType in implementationType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGeneric)
                {
                    return typeof(IMiraiHttpMessageParser);
                }
            }
            throw new ArgumentException($"给定的 {implementationType.Name} 不实现 {openGeneric.Name}", nameof(implementationType));
        }
    }
}
