using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mirai_CSharp.Utility.JsonConverters
{
    public class ChangeTypeJsonConverter<TSrc, TDst> : JsonConverter<TDst> where TSrc : TDst
    {
        public override TDst Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<TSrc>(ref reader, options)!;
        }

        public override void Write(Utf8JsonWriter writer, TDst value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<object>(writer, value!, options); // TDst : interface 时, 不使用object的泛型参数将导致基接口成员不会被序列化
        }
    }
}
