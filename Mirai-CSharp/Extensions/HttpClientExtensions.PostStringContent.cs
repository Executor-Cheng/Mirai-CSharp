using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Extensions
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="encoding">将 <paramref name="content"/> 处理到 <see cref="StringContent"/> 时要用的的一个 <see cref="Encoding"/>。 默认为 <see cref="Encoding.UTF8"/></param>
        /// <inheritdoc cref="SendAsync(HttpClient, HttpMethod, Uri, HttpContent?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri uri, string content, Encoding? encoding, CancellationToken token = default)
            => client.PostAsync(uri, new StringContent(content, encoding ?? Encoding.UTF8), token);

        /// <param name="url">请求目标</param>
        /// <inheritdoc cref="PostAsync(HttpClient, Uri, string, Encoding?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, string content, Encoding? encoding, CancellationToken token = default)
            => client.PostAsync(new Uri(url), content, encoding, token);

        /// <inheritdoc cref="PostAsync(HttpClient, Uri, string, Encoding?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri uri, string content, CancellationToken token = default)
            => client.PostAsync(uri, content, null, token);

        /// <inheritdoc cref="PostAsync(HttpClient, string, string, Encoding?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, string content, CancellationToken token = default)
            => client.PostAsync(url, content, null, token);
    }
}
