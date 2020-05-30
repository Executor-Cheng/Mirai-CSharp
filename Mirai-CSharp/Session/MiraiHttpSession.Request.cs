using Mirai_CSharp.Helpers;
using Mirai_CSharp.Utility;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private static void ProcessResponse(in JsonElement root)
        {
            int code = root.GetProperty("code").GetInt32();
            if (code != 0)
            {
                ThrowCommonException<object>(code, in root);
            }
        }

        private static TResult ProcessResponse<TResult>(in JsonElement root)
        {
            int code = root.GetProperty("code").GetInt32();
            if (code == 0)
            {
                return Utils.Deserialize<TResult>(in root);
            }
            return ThrowCommonException<TResult>(code, in root);
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
    }
}
