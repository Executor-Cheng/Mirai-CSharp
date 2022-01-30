using System;
#if !NET6_0_OR_GREATER
using System.Reflection;
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
            ReadOnlyMemory<byte> memory = GetRawdata(documentRef, idxPtr, true);
            return JsonSerializer.Deserialize<T>(memory.Span, options)!;
        }

        private static readonly unsafe delegate*<JsonDocument, void*, int, bool, ref ReadOnlyMemory<byte>> _jsonDocumentGetRawdataWinPtr;
        //                                            ^          ^     ^     ^            ^
        //                                            |          |     |     |            \-- ret
        //                                            |          |     |     \-- includeQuotes
        //                                            |          |     \-- idx
        //                                            \-- this   \-- &this
        // 751.1ns -> 577.1ns (-174ns, 76.83%) Alloc: -120B per invocation, this = JsonDocument
        // (调用实例方法, 且不用什么MethodInfo.Invoke, 还有什么Delegate。不同方法会有不同的参数列表, 请结合实际) 

        private static readonly unsafe delegate*<JsonDocument, int, bool, ReadOnlyMemory<byte>> _jsonDocumentGetRawdataUnixPtr;
        //                                            ^          ^     ^            ^
        //                                            |          |     |            \-- ret
        //                                            |          |     \-- includeQuotes
        //                                            |          \-- idx
        //                                            \-- this
        // this = JsonDocument

        static unsafe Utils()
        {
            IntPtr methodPtr = (IntPtr)typeof(Delegate).GetField("_methodPtr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(typeof(JsonDocument).GetMethod("GetRawValue", BindingFlags.NonPublic | BindingFlags.Instance)!.CreateDelegate(typeof(Func<int, bool, ReadOnlyMemory<byte>>), null))!;
            _jsonDocumentGetRawdataWinPtr = (delegate*<JsonDocument, void*, int, bool, ref ReadOnlyMemory<byte>>)methodPtr;
            _jsonDocumentGetRawdataUnixPtr = (delegate*<JsonDocument, int, bool, ReadOnlyMemory<byte>>)methodPtr;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static unsafe ReadOnlyMemory<byte> GetRawdata(JsonDocument j, int idx, bool includeQuotes)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return _jsonDocumentGetRawdataWinPtr(j, Unsafe.AsPointer(ref j), idx, includeQuotes);
            }
            return _jsonDocumentGetRawdataUnixPtr(j, idx, includeQuotes);
        }
#endif
    }
}
