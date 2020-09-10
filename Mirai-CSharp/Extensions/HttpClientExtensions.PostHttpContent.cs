using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Extensions
{
    public static partial class HttpClientExtensions
    {
        private static readonly string DefaultBoundary = $"MiraiCSharp/{Assembly.GetExecutingAssembly().GetName().Version}";

        /// <summary>
        /// 异步发起一个 HttpPost 请求
        /// </summary>
        /// <param name="contents">请求正文片段, 将以 multipart/form-data 的形式序列化</param>
        /// <inheritdoc cref="SendAsync(HttpClient, HttpMethod, Uri, HttpContent?, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri uri, IEnumerable<HttpContent> contents, CancellationToken token = default)
        {
            MultipartFormDataContent multipart = new MultipartFormDataContent(DefaultBoundary);
            foreach (HttpContent content in contents)
            {
                multipart.Add(content);
            }
            return client.PostAsync(uri, multipart, token);
        }

        /// <inheritdoc cref="PostAsync(HttpClient, Uri, IEnumerable{HttpContent}, CancellationToken)"/>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, IEnumerable<HttpContent> contents, CancellationToken token = default)
            => client.PostAsync(new Uri(url), contents, token);
    }
}
