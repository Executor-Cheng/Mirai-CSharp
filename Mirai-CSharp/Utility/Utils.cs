using System;
using System.Reflection;
using System.Text.Json;

namespace Mirai_CSharp.Utility
{
    public static class Utils
    {
        public static DateTime UnixTimeStamp2DateTime(int unixTimeStamp)
            => UnixTimeStamp2DateTime(unixTimeStamp * 1000L);

        public static DateTime UnixTimeStamp2DateTime(long unixTimeStamp)
            => new DateTime(unixTimeStamp * 10000 + 621355968000000000).ToLocalTime();

        public static int DateTime2UnixTimeStamp(DateTime time)
            => (int)(DateTime2UnixTimeStamp_Ms(time) / 1000);

        public static long DateTime2UnixTimeStamp_Ms(DateTime time)
            => (time.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        public static T Deserialize<T>(this in JsonElement element, JsonSerializerOptions? options = null)
        {
            var jsonDocument = JsonDeserialization.JsonDocumentField.GetValue(element);
            ReadOnlyMemory<byte> bytes = (ReadOnlyMemory<byte>)JsonDeserialization.JsonDocumentUtf8JsonField.GetValue(jsonDocument)!;
            return (T)JsonSerializer.Deserialize(bytes.Span, typeof(T), options)!;
        }

        private static class JsonDeserialization
        {
            public static readonly FieldInfo JsonDocumentField = typeof(JsonElement).GetField("_parent", BindingFlags.NonPublic | BindingFlags.Instance)!;
            public static readonly FieldInfo JsonDocumentUtf8JsonField = typeof(JsonDocument).GetField("_utf8Json", BindingFlags.NonPublic | BindingFlags.Instance)!;
        }
    }
}
