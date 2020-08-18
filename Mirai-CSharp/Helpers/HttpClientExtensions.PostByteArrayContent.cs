using System;
using System.Net.Http;
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
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri url, byte[] content, CancellationToken token = default)
            => client.HttpPostAsync(url, new ByteArrayContent(content), token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="HttpPostAsync(HttpClient, Uri, byte[], CancellationToken)"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, byte[] content, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), content, token);
    }
}
