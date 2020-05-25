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
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Assembly coreclr = Assembly.GetAssembly(typeof(object));
            UserAgent = $"Mirai-CSharp/{version.ToString(version.MinorRevision > 0 ? 4 : 3)} CoreCLR/{coreclr.GetCustomAttribute<AssemblyFileVersionAttribute>().Version}";
        }

        public static Task<JsonDocument> HttpGetAsync(string url, double timeout = 10, string userAgent = null, CancellationToken token = default)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Timeout = (int)(timeout * 1000);
            request.UserAgent = userAgent ?? UserAgent;
            return request.GetJsonDocumentAsync(token);
        }

        public static Task<JsonDocument> HttpPostAsync(string url, byte[] payload, double timeout = 10, string userAgent = null, CancellationToken token = default)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.Timeout = (int)(timeout * 1000);
            request.UserAgent = userAgent ?? UserAgent;
            using (Stream stream = request.GetRequestStream()) // 没必要用Async, 这个Stream实际上是MemoryStream实现的,
                                                               // MemoryStream类下的异步方法都是调用对应的同步方法, 然后返回
                                                               // default(ValueTask) / Task.CompletedTask
            {
                stream.Write(payload);
            }
            return request.GetJsonDocumentAsync(token);
        }

        public static async Task<JsonDocument> HttpPostAsync(string url, HttpContent[] contents, double timeout = 10, string userAgent = null, CancellationToken token = default)
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
            return await request.GetJsonDocumentAsync(token);
        }

        private static async Task<JsonDocument> GetJsonDocumentAsync(this HttpWebRequest request, CancellationToken token = default)
        {
            using WebResponse response = await request.GetResponseAsync().IgnoreNonSuccess();
            using Stream responseStream = response.GetResponseStream();
            return await JsonDocument.ParseAsync(responseStream, default, token);
        }

        public static Task<WebResponse> IgnoreNonSuccess(this Task<WebResponse> task)
        {
            return task.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception.InnerException is WebException e)
                {
                    return Task.FromResult(e.Response);
                }
                return t;
            }).Unwrap();
        }
    }
}