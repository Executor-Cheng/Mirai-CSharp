using System;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Framework.Attributes;

namespace Mirai.CSharp.Framework.Parsers.Attributes
{
    /// <summary>
    /// 标记一个消息类、消息接口或者消息处理类所需要使用的 <see cref="IMessageParser{TRawdata, TMessage}"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class RegisterParserAttribute : RegisterBaseAttribute
    {
        /// <summary>
        /// 使用给定的 <see cref="IMessageParser{TRawdata, TMessage}"/> 初始化 <see cref="RegisterParserAttribute"/> 的新实例
        /// </summary>
        /// <param name="implementationType"><see cref="IMessageParser{TRawdata, TMessage}"/> 的类型</param>
        public RegisterParserAttribute(Type implementationType) : this(implementationType, null)
        {

        }

        public RegisterParserAttribute(Type implementationType, ServiceLifetime? lifetime) : base(implementationType, lifetime)
        {

        }

        protected override Type GetServiceType(Type implementationType)
        {
            Type openGeneric = typeof(IMessageParser<,>);
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
