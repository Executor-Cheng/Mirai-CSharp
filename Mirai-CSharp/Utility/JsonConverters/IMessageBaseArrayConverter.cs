using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS0618 // 类型或成员已过时
namespace Mirai_CSharp.Utility.JsonConverters
{
    public class IMessageBaseArrayConverter : JsonConverter<IMessageBase[]>
    {
        public override IMessageBase[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<IMessageBase> result = new List<IMessageBase>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                JsonElement data = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
                result.Add(data.GetProperty("type").GetString() switch
                {
                    SourceMessage.MsgType => Utils.Deserialize<SourceMessage>(in data, options),
                    QuoteMessage.MsgType => Utils.Deserialize<QuoteMessage>(in data, options),
                    PlainMessage.MsgType => Utils.Deserialize<PlainMessage>(in data, options),
                    ImageMessage.MsgType => Utils.Deserialize<ImageMessage>(in data, options),
                    FlashImageMessage.MsgType => Utils.Deserialize<FlashImageMessage>(in data, options),
                    AtMessage.MsgType => Utils.Deserialize<AtMessage>(in data, options),
                    AtAllMessage.MsgType => Utils.Deserialize<AtAllMessage>(in data, options),
                    FaceMessage.MsgType => Utils.Deserialize<FaceMessage>(in data, options),
                    XmlMessage.MsgType => Utils.Deserialize<XmlMessage>(in data, options),
                    JsonMessage.MsgType => Utils.Deserialize<JsonMessage>(in data, options),
                    AppMessage.MsgType => Utils.Deserialize<AppMessage>(in data, options),
                    PokeMessage.MsgType => Utils.Deserialize<PokeMessage>(in data, options),
                    VoiceMessage.MsgType => Utils.Deserialize<VoiceMessage>(in data, options),
                    UnknownMessage.MsgType => Utils.Deserialize<UnknownMessage>(in data, options),
                    _ => throw new NotImplementedException("未知的消息类型:" + data.GetProperty("type").GetString())
                });
            }
            return result.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, IMessageBase[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (IMessageBase message in value)
            {
                JsonSerializer.Serialize(writer, message, message.GetType(), options);
            }
            writer.WriteEndArray();
        }
    }
}
