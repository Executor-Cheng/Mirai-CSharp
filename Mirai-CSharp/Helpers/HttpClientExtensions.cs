using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
namespace Mirai_CSharp.Helpers
{
    /// <summary>
    /// <see cref="HttpClient"/> 的扩展方法
    /// </summary>
    public static partial class HttpClientExtensions
    {
        private static Version DefaultHttpVersion { get; } = new Version(2, 0);

        /// <summary>
        /// 异步发起一个 Http 请求
        /// </summary>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="method">请求方式</param>
        /// <param name="uri">请求目标</param>
        /// <param name="content">请求正文</param>
        /// <param name="token">用于取消请求的 <see cref="CancellationToken"/></param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="HttpRequestException"/>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static async Task<HttpResponseMessage> PerformHttpRequestAsync(this HttpClient client, HttpMethod method, Uri uri, HttpContent? content, CancellationToken token = default)
        {
            using HttpRequestMessage request = new HttpRequestMessage(method, uri)
            {
                Content = content,
                Version = DefaultHttpVersion
            };
            return await client.SendAsync(request, token);
        }

        /// <summary>
        /// 将服务器响应正文异步读取为 <see cref="byte"/>[]
        /// </summary>
        /// <returns></returns>
        public static async Task<byte[]> GetBytesAsync(this Task<HttpResponseMessage> responseTask)
        {
            using HttpResponseMessage response = await responseTask;
            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// 将服务器响应正文异步读取为 <see cref="string"/>。 <see cref="Encoding"/> 由服务器响应头中的 Content-Type 决定
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(this Task<HttpResponseMessage> responseTask)
        {
            using HttpResponseMessage response = await responseTask;
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 将服务器响应正文异步读取为 <see cref="string"/>
        /// </summary>
        /// <param name="responseTask">要处理的一个异步请求任务</param>
        /// <param name="encoding">用于编码响应正文的 <see cref="Encoding"/>。为 <see langword="null"/> 时将使用 <see cref="Encoding.UTF8"/></param>
        /// <param name="token">用于取消异步读取的 <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(this Task<HttpResponseMessage> responseTask, Encoding? encoding, CancellationToken token = default)
        {
            using HttpResponseMessage response = await responseTask;
#if NET5_0
            using Stream stream = await response.Content.ReadAsStreamAsync(token);
#else
            using Stream stream = await response.Content.ReadAsStreamAsync();
#endif
            using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// 将服务器响应正文异步反序列化为给定的 <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseTask">要处理的一个异步请求任务</param>
        /// <param name="options">反序列化时用到的 <see cref="JsonSerializerOptions"/></param>
        /// <param name="token">用于取消反序列化的 <see cref="CancellationToken"/></param>
        /// <remarks>
        /// 非 .NET 5.0 使用本扩展方法请确保服务器响应的 Json 是以 UTF-8 编码的
        /// </remarks>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static async Task<T> GetObjectAsync<T>(this Task<HttpResponseMessage> responseTask, JsonSerializerOptions? options, CancellationToken token = default)
        {
            using HttpResponseMessage response = await responseTask.ConfigureAwait(false);
#if NET5_0
            return await response.Content.ReadFromJsonAsync<T>(options, token);
#else
            using Stream stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, options, token);
#endif
        }

        /// <summary>
        /// 将服务器响应正文异步反序列化为给定的 <paramref name="returnType"/>
        /// </summary>
        /// <param name="returnType">用于转换和返回的 <see cref="Type"/></param>
        /// <inheritdoc cref="GetObjectAsync{T}(Task{HttpResponseMessage}, JsonSerializerOptions?, CancellationToken)"/>
        public static async Task<object?> GetObjectAsync(this Task<HttpResponseMessage> responseTask, Type returnType, JsonSerializerOptions? options, CancellationToken token = default)
        {
            using HttpResponseMessage response = await responseTask;
#if NET5_0
            return await response.Content.ReadFromJsonAsync(returnType, options, token);
#else
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync(stream, returnType, options, token);
#endif
        }

        /// <summary>
        /// 将服务器响应正文异步解析到 <see cref="JsonDocument"/>
        /// </summary>
        /// <inheritdoc cref="GetJsonAsync(Task{HttpResponseMessage}, JsonDocumentOptions, CancellationToken)"/>
        public static Task<JsonDocument> GetJsonAsync(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            return responseTask.GetJsonAsync(default, token);
        }

        /// <summary>
        /// 将服务器响应正文异步解析到 <see cref="JsonDocument"/>
        /// </summary>
        /// <remarks>
        /// 返回的 <see cref="JsonDocument"/> 需要你在使用完毕后调用 <see cref="JsonDocument.Dispose"/> 方法释放资源
        /// </remarks>
        /// <param name="responseTask">要处理的一个异步请求任务</param>
        /// <param name="options">将在解析时使用的 <see cref="JsonDocumentOptions"/></param>
        /// <param name="token">用于取消解析的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static async Task<JsonDocument> GetJsonAsync(this Task<HttpResponseMessage> responseTask, JsonDocumentOptions options, CancellationToken token = default)
        {
            using HttpResponseMessage response = await responseTask;
#if NET5_0
            using Stream stream = await response.Content.ReadAsStreamAsync(token);
#else
            using Stream stream = await response.Content.ReadAsStreamAsync();
#endif
            return await JsonDocument.ParseAsync(stream, options, token);
        }

        /// <summary>
        /// 如果给定的 <paramref name="task"/> 抛出异常, 则返回 <see langword="null"/>
        /// </summary>
        /// <returns></returns>
        internal static Task<T?> NullIfFailed<T>(this Task<T?> task) where T : class
            => task.ContinueWith(t => t.IsFaulted ? Task.FromResult<T?>(null) : t, TaskContinuationOptions.ExecuteSynchronously).Unwrap();
    }
}