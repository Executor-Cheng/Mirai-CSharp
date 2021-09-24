using System;
using System.Reflection;
using System.Text.Json;
using Mirai.CSharp.HttpApi.JsonServices;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility;

namespace Mirai.CSharp.HttpApi.Parsers
{
    public class DefaultMappableMiraiHttpMessageParser<TMessage, TImpl> : MappableMiraiHttpMessageParser<TMessage> where TMessage : IMiraiHttpMessage
                                                                                                                   where TImpl : MiraiHttpMessage, TMessage, new()
    {
        private readonly JsonSerializerOptions? _options;

        public override string Key { get; }

        public DefaultMappableMiraiHttpMessageParser(IMiraiHttpMessageJsonOptionsFactory? factory)
        {
            Key = typeof(TImpl).GetCustomAttribute<MappableMiraiHttpMessageKeyAttribute>()?.Key ??
                  typeof(TMessage).GetCustomAttribute<MappableMiraiHttpMessageKeyAttribute>()?.Key ??
                  throw new InvalidOperationException($"给定的实现类型 {typeof(TImpl)} 和接口类型 {typeof(TMessage)} 均未标注 MappableMiraiHttpMessageKeyAttribute.");
            if (factory != null)
            {
                _options = factory.GetOptions<TImpl>()?.Options ?? factory.GetOptions<TMessage>()?.Options;
            }
        }

        public override TMessage Parse(in JsonElement root)
        {
            TImpl result = root.Deserialize<TImpl>(_options)!;
            result.Rawdata = root;
            return result;
        }
    }
}
