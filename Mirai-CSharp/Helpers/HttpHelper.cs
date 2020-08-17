using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Helpers
{
    public static class HttpHelper
    {
        public static string UserAgent { get; }

        static HttpHelper()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version!;
            Assembly coreclr = Assembly.GetAssembly(typeof(object))!;
            UserAgent = $"Mirai-CSharp/{version.ToString(version.MinorRevision > 0 ? 4 : 3)} CoreCLR/{coreclr.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version}";
        }

        public static Task<WebResponse> HttpGetAsync(string url, double timeout = 10, string? userAgent = null)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Timeout = (int)(timeout * 1000);
            request.UserAgent = userAgent ?? UserAgent;
            return request.GetResponseAsync();
        }

        public static Task<WebResponse> HttpPostAsync(string url, byte[] payload, double timeout = 10, string? userAgent = null)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.Timeout = (int)(timeout * 1000);
            request.UserAgent = userAgent ?? UserAgent;
            using (Stream stream = request.GetRequestStream()) // 没必要用Async, 这个Stream实际上是MemoryStream实现的,
                                                               // MemoryStream类下的异步方法都是调用对应的同步方法, 然后返回
                                                               // default(ValueTask) / Task.CompletedTask
            {
#if NETSTANDARD2_0
                stream.Write(payload, 0, payload.Length);
#else
                stream.Write(payload);
#endif
            }
            return request.GetResponseAsync();
        }

        public static async Task<WebResponse> HttpPostAsync(string url, HttpContent[] contents, double timeout = 10, string? userAgent = null)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.Timeout = (int)(timeout * 1000);
            request.UserAgent = userAgent ?? UserAgent;
            request.ContentType = "multipart/form-data; boundary=MiraiCSharp";
            using (MultipartFormDataContent multipart = new MultipartFormDataContent("MiraiCSharp"))
            {
                foreach (HttpContent content in contents)
                {
                    multipart.Add(content);
                }
                using Stream stream = request.GetRequestStream();
                using Stream multipartStream = await multipart.ReadAsStreamAsync();
                await multipartStream.CopyToAsync(stream);
            }
            return await request.GetResponseAsync();
        }

        public static async Task<string> GetStringAsync(this WebResponse response, Encoding? encoding = null, bool disposeResponse = true)
        {
            try
            {
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
                return await reader.ReadToEndAsync(); // 没有 CancellationToken 的重载
            }
            finally
            {
                if (disposeResponse)
                {
                    response.Dispose();
                }
            }
        }
        public static async Task<string> GetStringAsync(this Task<WebResponse> responseTask, Encoding? encoding = null)
        {
            using WebResponse response = await responseTask.ConfigureAwait(false);
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
            return await reader.ReadToEndAsync(); // 没有 CancellationToken 的重载, 禁止去掉async/await, 会导致Stream被释放, 创建状态机无法避免
        }

        public static async Task<JsonDocument> GetJsonAsync(this WebResponse response, JsonDocumentOptions options = default, bool disposeResponse = true, CancellationToken token = default)
        {
            try
            {
                using Stream stream = response.GetResponseStream();
                return await JsonDocument.ParseAsync(stream, options, token);
            }
            finally
            {
                if (disposeResponse)
                {
                    response.Dispose();
                }
            }
        }
        public static async Task<JsonDocument> GetJsonAsync(this Task<WebResponse> responseTask, JsonDocumentOptions options = default, CancellationToken token = default)
        {
            using WebResponse response = await responseTask.ConfigureAwait(false);
            using Stream stream = response.GetResponseStream();
            return await JsonDocument.ParseAsync(stream, options, token);
        }

        public static Task<WebResponse> IgnoreNonSuccess(this Task<WebResponse> task)
        {
            return task.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception!.InnerException is WebException e && e.Response != null)
                {
                    return Task.FromResult(e.Response);
                }
                return t;
            }).Unwrap();
        }
    }
}