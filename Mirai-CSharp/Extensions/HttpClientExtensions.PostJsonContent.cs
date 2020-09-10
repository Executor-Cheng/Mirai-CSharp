#if !NET5_0
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Extensions
{
    public static partial class HttpClientExtensions
    {
        private static readonly MediaTypeHeaderValue DefaultJsonMediaType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };

        /// <inheritdoc cref="PostAsJsonAsync{TValue}(HttpClient, Uri, TValue, JsonSerializerOptions?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpClient client, Uri uri, TValue value, CancellationToken token = default)
        {
            return client.PostAsJsonAsync(uri, value, null, token);
        }

        /// <inheritdoc cref="PostAsJsonAsync{TValue}(HttpClient, string, TValue, JsonSerializerOptions?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpClient client, string url, TValue value, CancellationToken token = default)
        {
            return client.PostAsJsonAsync(url, value, null, token);
        }

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="value">作为 Json 正文的对象</param>
        /// <param name="options">序列化 <paramref name="value"/> 时要用到的 <see cref="JsonSerializerOptions"/></param>
        /// <inheritdoc cref="PostAsync(HttpClient, Uri, byte[], CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpClient client, Uri uri, TValue value, JsonSerializerOptions? options, CancellationToken token = default)
        {
            ByteArrayContent content = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(value, options));
            content.Headers.ContentType = DefaultJsonMediaType;
            return client.PostAsync(uri, content, token);
        }

        /// <param name="url">请求目标</param>
        /// <inheritdoc cref="PostAsJsonAsync{TValue}(HttpClient, Uri, TValue, JsonSerializerOptions?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpClient client, string url, TValue value, JsonSerializerOptions? options, CancellationToken token = default)
            => client.PostAsJsonAsync(new Uri(url), value, options, token);
    }
}
#endif