using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Extensions
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="SendAsync(HttpClient, HttpMethod, Uri, HttpContent?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri uri, byte[] content, CancellationToken token = default)
            => client.PostAsync(uri, new ByteArrayContent(content), token);

        /// <inheritdoc cref="PostAsync(HttpClient, Uri, byte[], CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, byte[] content, CancellationToken token = default)
            => client.PostAsync(new Uri(url), content, token);
    }
}
