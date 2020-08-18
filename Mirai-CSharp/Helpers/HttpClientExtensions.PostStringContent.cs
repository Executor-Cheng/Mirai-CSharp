using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Helpers
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="encoding">编码 <paramref name="content"/> 时要使用的 <see cref="Encoding"/></param>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri url, string content, Encoding? encoding, CancellationToken token = default)
            => client.HttpPostAsync(url, new StringContent(content, encoding), token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="HttpPostAsync(HttpClient, Uri, string, Encoding?, CancellationToken)"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, string content, Encoding? encoding, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), content, encoding, token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri url, string content, CancellationToken token = default)
            => client.HttpPostAsync(url, content, (Encoding?)null, token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, string content, CancellationToken token = default)
            => client.HttpPostAsync(url, content, (Encoding?)null, token);
    }
}
