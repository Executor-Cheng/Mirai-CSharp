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
        /// <inheritdoc cref="PostAsync(HttpClient, Uri, byte[], CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri uri, CancellationToken token = default)
            => client.PostAsync(uri, null!, token);

        /// <inheritdoc cref="PostAsync(HttpClient, string, byte[], CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, CancellationToken token = default)
            => client.PostAsync(new Uri(url), token);
    }
}
