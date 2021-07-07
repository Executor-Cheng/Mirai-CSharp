using Mirai_CSharp.Utility;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Extensions
{
    internal static class ApiResponseExtensions // internal 的 class, 以后再考虑加点 JsonDocumentOptions, JsonSerializerOptions 的重载
    {
#if NETSTANDARD2_0
        public static bool CheckApiRespCode(this JsonElement root, out int? code)
#else
        public static bool CheckApiRespCode(this JsonElement root, [NotNullWhen(false)] out int? code)
#endif
        {
            // 自v1.9.0起, 可能所有api返回成功时都不带code属性了。
            // 那么一个失败的api返回一定是: root是Object, 有code属性, 且code!=0
            if (root.ValueKind != JsonValueKind.Object || !root.TryGetProperty("code", out JsonElement codeToken) || codeToken.GetInt32() == 0)
            {
                code = null;
                return true;
            }
            code = codeToken.GetInt32();
            return false;
        }

        public static async Task AsApiRespAsync(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            using JsonDocument j = await responseTask.GetJsonAsync(token);
            JsonElement root = j.RootElement;
            if (!root.CheckApiRespCode(out int? code))
            {
                throw MiraiHttpSession.GetCommonException(code!.Value, in root);
            }
        }

        public static Task<TResult> AsApiRespAsync<TResult>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            return responseTask.AsApiRespAsync<TResult, TResult>(token);
        }

        public static async Task<TResult> AsApiRespAsync<TResult, TImpl>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default) where TImpl : TResult
        {
            using JsonDocument j = await responseTask.GetJsonAsync(token);
            JsonElement root = j.RootElement;
            if (root.CheckApiRespCode(out int? code))
            {
                return root.Deserialize<TImpl>();
            }
            throw MiraiHttpSession.GetCommonException(code!.Value, in root);
        }
    }
}
