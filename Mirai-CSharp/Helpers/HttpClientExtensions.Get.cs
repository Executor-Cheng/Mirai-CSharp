using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Helpers
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpGet 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpGetAsync(this HttpClient client, Uri uri, CancellationToken token = default)
            => client.PerformHttpRequestAsync(HttpMethod.Get, uri, null, token);

        /// <summary>
        /// 异步发起一个 HttpGet 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpGetAsync(this HttpClient client, string url, CancellationToken token = default)
            => client.HttpGetAsync(new Uri(url), token);
    }
}
