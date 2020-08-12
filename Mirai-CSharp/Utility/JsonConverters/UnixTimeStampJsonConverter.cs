using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Utility.JsonConverters
{
    public class UnixTimeStampJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Number => Utils.UnixTimeStamp2DateTime(reader.GetInt32()),
                JsonTokenType.String => DateTime.Parse(reader.GetString()!),
                _ => throw new ArgumentException($"Expected unix timestamp or datetime string, got {reader.TokenType}")
            };
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Utils.DateTime2UnixTimeStamp(value));
        }
    }
}
