#if NETCOREAPP3_0 || NETCOREAPP3_1
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Utility.JsonConverters
{
    public sealed class NullableJsonConverter<T, TConverter> : JsonConverter<T?> where T : struct
                                                                                 where TConverter : JsonConverter<T>, new()
    {
        private readonly TConverter _converter;

        public NullableJsonConverter()
        {
            _converter = new TConverter();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return _converter.CanConvert(typeToConvert.GetGenericArguments()[0]);
            }
            return _converter.CanConvert(typeToConvert);
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            T value = _converter.Read(ref reader, typeof(T), options);
            return value;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }
            _converter.Write(writer, value.Value, options);
        }
    }
}
#endif
