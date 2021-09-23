using System.Text.Json;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Mirai.CSharp.HttpApi.Utility
{
    public static class JsonSerializeOptionsFactory
    {
#if NET6_0_OR_GREATER
        public static JsonSerializerOptions IgnoreNulls => new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
#else
        public static JsonSerializerOptions IgnoreNulls => new JsonSerializerOptions { IgnoreNullValues = true };
#endif
    }
}
