using Mirai_CSharp.Extensions;
using Mirai_CSharp.Utility;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Extensions
{
    internal static class ApiResponseExtensions // internal 的 class, 以后再考虑加点 JsonDocumentOptions, JsonSerializerOptions 的重载
    {
        public static async Task AsNoSuccCodeApiRespAsync(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            using JsonDocument j = await responseTask.GetJsonAsync(token);
            JsonElement root = j.RootElement;
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("code", out JsonElement codeElem))
            {
                int code = codeElem.GetInt32();
                if (code != 0)
                {
                    throw MiraiHttpSession.GetCommonException(code, in root);
                }
            }
        }

        public static Task<TResult> AsNoSuccCodeApiRespAsync<TResult>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            return responseTask.AsNoSuccCodeApiRespAsync<TResult, TResult>(token);
        }

        public static async Task<TResult> AsNoSuccCodeApiRespAsync<TResult, TImpl>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default) where TImpl : TResult
        {
            using JsonDocument j = await responseTask.GetJsonAsync(token);
            JsonElement root = j.RootElement;
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("code", out JsonElement codeElem))
            {
                int code = codeElem.GetInt32();
                if (code != 0)
                {
                    throw MiraiHttpSession.GetCommonException(code, in root);
                }
            }
            return root.Deserialize<TImpl>();
        }

        public static async Task AsApiRespAsync(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            using JsonDocument j = await responseTask.GetJsonAsync(token);
            JsonElement root = j.RootElement;
            int code = root.GetProperty("code").GetInt32();
            if (code != 0)
            {
                throw MiraiHttpSession.GetCommonException(code, in root);
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
            int code = root.GetProperty("code").GetInt32();
            if (code == 0)
            {
                return root.Deserialize<TImpl>();
            }
            throw MiraiHttpSession.GetCommonException(code, in root);
        }
    }
}
