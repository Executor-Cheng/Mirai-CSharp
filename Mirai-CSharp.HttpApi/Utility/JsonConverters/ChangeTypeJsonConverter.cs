using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Utility.JsonConverters
{
    public class ChangeTypeJsonConverter<TFrom, TTo> : ChangeTypeJsonConverter<TFrom, object, TTo> where TFrom : notnull where TTo : TFrom
    {
        
    }

    public class ChangeTypeJsonConverter<TFrom, TSerializeTo, TDeserializeTo> : JsonConverter<TFrom> where TDeserializeTo : TFrom where TFrom : notnull, TSerializeTo
    {
        public override TFrom Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<TDeserializeTo>(ref reader, options)!;
        }

        public override void Write(Utf8JsonWriter writer, TFrom value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<TSerializeTo>(writer, value!, options);
        }
    }
}
