using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Helpers
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri uri, CancellationToken token = default)
            => client.HttpPostAsync(uri, (HttpContent?)null, token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), (HttpContent?)null, token);
    }
}
