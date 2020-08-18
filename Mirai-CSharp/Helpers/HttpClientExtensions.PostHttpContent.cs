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
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri uri, HttpContent? content, CancellationToken token = default)
            => client.PerformHttpRequestAsync(HttpMethod.Post, uri, content, token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, HttpContent? content, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), content, token);

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="contents">请求正文片段</param>
        /// <inheritdoc cref="PerformHttpRequestAsync"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, Uri uri, HttpContent[] contents, CancellationToken token = default)
        {
            MultipartFormDataContent multipart = new MultipartFormDataContent("MiraiCSharp");
            foreach (HttpContent content in contents)
            {
                multipart.Add(content);
            }
            return client.PerformHttpRequestAsync(HttpMethod.Post, uri, multipart, token);
        }

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <inheritdoc cref="HttpPostAsync(HttpClient, Uri, HttpContent[], CancellationToken)"/>
        public static Task<HttpResponseMessage> HttpPostAsync(this HttpClient client, string url, HttpContent[] contents, CancellationToken token = default)
            => client.HttpPostAsync(new Uri(url), contents, token);
    }
}
