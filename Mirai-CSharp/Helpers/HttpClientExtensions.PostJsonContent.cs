using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Helpers
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="value">作为 Json 正文的对象</param>
        /// <param name="options">序列化 <typeparamref name="TValue"/> 时要用到的 <see cref="JsonSerializerOptions"/></param>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync<TValue>(this HttpClient client, Uri uri, TValue value, JsonSerializerOptions? options, CancellationToken token = default)
#if NET5_0
            => client.PostAsJsonAsync(uri, value, token);
#else
            => client.HttpPostAsync(uri, new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(value)), token);
#endif
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="url">请求目标</param>
        /// <param name="value">作为 Json 正文的对象</param>
        /// <inheritdoc cref="HttpPostAsync{TValue}(HttpClient, Uri, TValue, JsonSerializerOptions?, CancellationToken)"/>
        public static Task<HttpResponseMessage> HttpPostAsync<TValue>(this HttpClient client, string url, TValue value, JsonSerializerOptions? options, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), value, options, token);
    }
}
