using Mirai_CSharp.Helpers;
using Mirai_CSharp.Utility;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private static void ProcessNoSuccCodeResponse(in JsonElement root)
        {
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("code", out JsonElement codeElem))
            {
                int code = codeElem.GetInt32();
                if (code != 0)
                {
                    throw GetCommonException(code, in root);
                }
            }
        }

        private static TResult ProcessNoSuccCodeResponse<TResult>(in JsonElement root)
        {
            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("code", out JsonElement codeElem))
            {
                int code = codeElem.GetInt32();
                if (code != 0)
                {
                    throw GetCommonException(code, in root);
                }
            }
            return root.Deserialize<TResult>();
        }

        private static void ProcessResponse(in JsonElement root)
        {
            int code = root.GetProperty("code").GetInt32();
            if (code != 0)
            {
                throw GetCommonException(code, in root);
            }
        }

        private static TResult ProcessResponse<TResult>(in JsonElement root)
        {
            int code = root.GetProperty("code").GetInt32();
            if (code == 0)
            {
                return root.Deserialize<TResult>();
            }
            throw GetCommonException(code, in root);
        }

        private static async Task InternalHttpGetAsync(string url, CancellationToken token = default)
        {
            using JsonDocument j = await HttpHelper.HttpGetAsync(url).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            ProcessResponse(in root);
        }

        private static async Task<TResult> InternalHttpGetAsync<TResult, TImpl>(string url, CancellationToken token = default) where TImpl : class, TResult
        {
            using JsonDocument j = await HttpHelper.HttpGetAsync(url).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            return ProcessResponse<TImpl>(in root);
        }

        private static async Task InternalHttpGetNoSuccCodeAsync(string url, CancellationToken token = default)
        {
            using JsonDocument j = await HttpHelper.HttpGetAsync(url).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            ProcessNoSuccCodeResponse(in root);
        }

        private static async Task<TResult> InternalHttpGetNoSuccCodeAsync<TResult, TImpl>(string url, CancellationToken token = default) where TImpl : class, TResult
        {
            using JsonDocument j = await HttpHelper.HttpGetAsync(url).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            return ProcessNoSuccCodeResponse<TImpl>(in root);
        }

        private static async Task InternalHttpPostAsync(string url, byte[] payload, CancellationToken token = default)
        {
            using JsonDocument j = await HttpHelper.HttpPostAsync(url, payload).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            ProcessResponse(in root);
        }

        private static async Task<TResult> InternalHttpPostAsync<TResult, TImpl>(string url, byte[] payload, CancellationToken token = default) where TImpl : class, TResult
        {
            using JsonDocument j = await HttpHelper.HttpPostAsync(url, payload).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            return ProcessResponse<TImpl>(in root);
        }

        private static async Task InternalHttpPostNoSuccCodeAsync(string url, byte[] payload, CancellationToken token = default)
        {
            using JsonDocument j = await HttpHelper.HttpPostAsync(url, payload).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            ProcessNoSuccCodeResponse(in root);
        }

        private static async Task<TResult> InternalHttpPostNoSuccCodeAsync<TResult, TImpl>(string url, byte[] payload, CancellationToken token = default) where TImpl : class, TResult
        {
            using JsonDocument j = await HttpHelper.HttpPostAsync(url, payload).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            return ProcessNoSuccCodeResponse<TImpl>(in root);
        }

        private static async Task<TResult> InternalHttpPostNoSuccCodeAsync<TResult, TImpl>(string url, HttpContent[] contents, CancellationToken token = default) where TImpl : class, TResult
        {
            using JsonDocument j = await HttpHelper.HttpPostAsync(url, contents).GetJsonAsync(token: token);
            JsonElement root = j.RootElement;
            return ProcessNoSuccCodeResponse<TImpl>(in root);
        }
    }
}
