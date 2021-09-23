using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Parsers.Attributes;

namespace Mirai.CSharp.HttpApi.JsonServices
{
    public interface IMiraiHttpMessageJsonOptionsFactory
    {
        IMiraiHttpMessageJsonOptions<TMessage>? GetOptions<TMessage>() where TMessage : IMiraiHttpMessage;

        IMiraiHttpMessageJsonOptions? GetOptions(Type messageType);
    }

    public class MiraiHttpMessageJsonOptionsFactory : IMiraiHttpMessageJsonOptionsFactory
    {
        private readonly IServiceProvider _services;

        public MiraiHttpMessageJsonOptionsFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IMiraiHttpMessageJsonOptions? GetOptions(Type messageType)
        {
            if (!messageType.IsAssignableFrom(typeof(IMiraiHttpMessage)))
            {
                throw new InvalidOperationException($"给定的 {messageType} 不实现 {typeof(IMiraiHttpMessage)}.");
            }
            if (messageType.IsDefined(typeof(ResolveJsonConverterAttribute)))
            {
                Type optionsType = typeof(IMiraiHttpMessageJsonOptions<>).MakeGenericType(messageType);
                return (IMiraiHttpMessageJsonOptions?)_services.GetRequiredService(optionsType);
            }
            return null;
        }

        public IMiraiHttpMessageJsonOptions<TMessage>? GetOptions<TMessage>() where TMessage : IMiraiHttpMessage
        {
            if (typeof(TMessage).IsDefined(typeof(ResolveJsonConverterAttribute)))
            {
                return _services.GetRequiredService<IMiraiHttpMessageJsonOptions<TMessage>>();
            }
            return null;
        }
    }
}
