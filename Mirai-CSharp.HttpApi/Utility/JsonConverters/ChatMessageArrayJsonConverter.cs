using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers;

#if !NET5_0_OR_GREATER
#pragma warning disable CS8764 // Nullability of return type doesn't match overridden member (possibly because of nullability attributes).
#endif
namespace Mirai.CSharp.HttpApi.Utility.JsonConverters
{
    public class ChatMessageJsonConverter : JsonConverter<IChatMessage>
    {
        private readonly IServiceProvider _services;

        public ChatMessageJsonConverter(IServiceProvider services)
        {
            _services = services;
        }

        public override IChatMessage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawdata = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            IMiraiHttpChatMessageParserResolver resolver = _services.GetRequiredService<IMiraiHttpChatMessageParserResolver>(); // 迟点解析, 避免循环依赖
            IEnumerable<IMiraiHttpChatMessageParser> parsers = resolver.ResolveParsers(in rawdata);
            bool unknownWasUsed = false;
            while (true)
            {
                foreach (var parser in parsers)
                {
                    if (parser.CanParse(in rawdata))
                    {
                        return (IChatMessage)parser.Parse(in rawdata);
                    }
                }
                if (unknownWasUsed) // 有没用过未知消息的解析器
                {
                    break;
                }
                unknownWasUsed = true;
                parsers = resolver.UnknownMessageParsers;
            }
            throw new InvalidOperationException("未能解析此消息");
        }

        public override void Write(Utf8JsonWriter writer, IChatMessage value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
