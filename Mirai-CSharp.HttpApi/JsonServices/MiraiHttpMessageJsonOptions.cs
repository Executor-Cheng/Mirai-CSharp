using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Parsers.Attributes;

namespace Mirai.CSharp.HttpApi.JsonServices
{
    public interface IMiraiHttpMessageJsonOptions
    {
        JsonSerializerOptions? Options { get; }
    }

    public interface IMiraiHttpMessageJsonOptions<TMessage> : IMiraiHttpMessageJsonOptions where TMessage : IMiraiHttpMessage
    {
        
    }

    public class MiraiHttpMessageJsonOptions<TMessage> : IMiraiHttpMessageJsonOptions<TMessage> where TMessage : IMiraiHttpMessage
    {
        public JsonSerializerOptions? Options { get; }

        public MiraiHttpMessageJsonOptions(IServiceProvider services)
        {
            Options = new JsonSerializerOptions();
            foreach (ResolveJsonConverterAttribute attribute in typeof(TMessage).GetCustomAttributes<ResolveJsonConverterAttribute>())
            {
                Options.Converters.Add((JsonConverter)services.GetRequiredService(attribute.ServiceType));
            }
        }
    }
}
