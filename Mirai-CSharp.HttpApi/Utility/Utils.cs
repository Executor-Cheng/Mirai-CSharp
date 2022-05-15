using System;
#if !NET6_0_OR_GREATER
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.Json;
#endif

namespace Mirai.CSharp.HttpApi.Utility
{
    public static class Utils
    {
        public static DateTime UnixTime2DateTime(int unixTimeStamp)
            => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime.ToLocalTime();

        public static DateTime UnixTime2DateTime(long unixTimeStamp)
            => DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).DateTime.ToLocalTime();

        public static int DateTime2UnixTimeSeconds(DateTime time)
            => (int)new DateTimeOffset(time).ToUnixTimeSeconds();

        public static long DateTime2UnixTimeMillseconds(DateTime time)
            => new DateTimeOffset(time).ToUnixTimeMilliseconds();

#if !NET6_0_OR_GREATER
        public static T Deserialize<T>(this JsonElement element, JsonSerializerOptions? options = null)
        {
            JsonDocument documentRef = Unsafe.As<JsonElement, JsonDocument>(ref element);
            int idxPtr = Unsafe.AddByteOffset(ref Unsafe.As<JsonElement, int>(ref element), new IntPtr(IntPtr.Size));
            ReadOnlyMemory<byte> memory = _jsonDocumentGetRawdataDelegate(documentRef, idxPtr, true);
            return JsonSerializer.Deserialize<T>(memory.Span, options)!;
        }

        private static readonly Func<JsonDocument, int, bool, ReadOnlyMemory<byte>> _jsonDocumentGetRawdataDelegate;

        static unsafe Utils()
        {
            DynamicMethod method = new DynamicMethod("", typeof(ReadOnlyMemory<byte>), new Type[] { typeof(JsonDocument), typeof(int), typeof(bool) }, typeof(JsonDocument), true);
            ILGenerator generator = method.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(OpCodes.Call, typeof(JsonDocument).GetMethod("GetRawValue", BindingFlags.NonPublic | BindingFlags.Instance)!);
            generator.Emit(OpCodes.Ret);
            _jsonDocumentGetRawdataDelegate = (Func<JsonDocument, int, bool, ReadOnlyMemory<byte>>)method.CreateDelegate(typeof(Func<JsonDocument, int, bool, ReadOnlyMemory<byte>>));
        }
#endif
    }
}
