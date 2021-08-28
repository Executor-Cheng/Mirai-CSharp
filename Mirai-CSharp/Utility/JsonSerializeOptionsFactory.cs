using System.Text.Json;

namespace Mirai.CSharp.Utility
{
    public static class JsonSerializeOptionsFactory
    {
        public static JsonSerializerOptions IgnoreNulls => new JsonSerializerOptions { IgnoreNullValues = true };
    }
}
