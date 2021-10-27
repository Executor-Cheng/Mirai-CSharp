using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Exceptions;
using Mirai.CSharp.HttpApi.Exceptions;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.HttpApi.Utility;

namespace Mirai.CSharp.HttpApi.Extensions
{
    internal static class ApiResponseExtensions // internal 的 class, 以后再考虑加点 JsonDocumentOptions, JsonSerializerOptions 的重载
    {
        /// <summary>
        /// 通过状态码返回相应的异常
        /// </summary>
        /// <returns>
        /// 根据给定的 <paramref name="code"/> 返回下列异常之一:
        /// <list type="bullet">
        /// <item><term><see cref="InvalidAuthKeyException"/></term><description><paramref name="code"/> 为 1</description></item>
        /// <item><term><see cref="BotNotFoundException"/></term><description><paramref name="code"/> 为 2</description></item>
        /// <item><term><see cref="InvalidSessionException"/></term><description><paramref name="code"/> 为 3 或 4</description></item>
        /// <item><term><see cref="TargetNotFoundException"/></term><description><paramref name="code"/> 为 5</description></item>
        /// <item><term><see cref="FileNotFoundException"/></term><description><paramref name="code"/> 为 6</description></item>
        /// <item><term><see cref="PermissionDeniedException"/></term><description><paramref name="code"/> 为 10</description></item>
        /// <item><term><see cref="BotMutedException"/></term><description><paramref name="code"/> 为 20</description></item>
        /// <item><term><see cref="MessageTooLongException"/></term><description><paramref name="code"/> 为 30</description></item>
        /// <item><term><see cref="ArgumentException"/></term><description><paramref name="code"/> 为 400</description></item>
        /// <item><term><see cref="UnknownResponseException"/></term><description>其它情况</description></item>
        /// </list>
        /// </returns>
        internal static Exception GetCommonException(int code, in JsonElement root)
        {
            return code switch
            {
                1 => new InvalidAuthKeyException(),
                2 => new BotNotFoundException(),
                // 3 or 4 => new InvalidSessionException(), // C# 9.0
                3 => new InvalidSessionException(),
                4 => new InvalidSessionException(),
                5 => new TargetNotFoundException(),
                6 => new FileNotFoundException("指定的文件不存在。"),
                10 => new PermissionDeniedException(),
                20 => new BotMutedException(),
                30 => new MessageTooLongException(),
                400 => new ArgumentException("调用http-api失败, 参数错误, 请到 https://github.com/Executor-Cheng/Mirai-CSharp/issues 下提交issue。"),
                _ => new UnknownResponseException(root.GetRawText())
            };
        }

#if NETSTANDARD2_0
        public static bool CheckApiRespCode(this JsonElement root, out int? code)
#else
        public static bool CheckApiRespCode(this JsonElement root, [NotNullWhen(false)] out int? code)
#endif
        {
            // 自v1.9.0起, 可能所有api返回成功时都不带code属性了。
            // 那么一个失败的api返回一定是: root是Object, 有code属性, 且code!=0
            if (root.ValueKind != JsonValueKind.Object || !root.TryGetProperty("code", out var codeToken) || codeToken.GetInt32() == 0)
            {
                code = null;
                return true;
            }
            code = codeToken.GetInt32();
            return false;
        }

        public static void EnsureApiRespCode(this JsonElement root)
        {
            if (!root.CheckApiRespCode(out int? code))
            {
                throw GetCommonException(code!.Value, in root);
            }
        }

        public static async Task AsApiRespAsync(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            using var j = await responseTask.GetJsonAsync(token);
            var root = j.RootElement;
            if (!root.CheckApiRespCode(out var code))
            {
                throw GetCommonException(code!.Value, in root);
            }
        }

        public static Task<TResult> AsApiRespAsync<TResult>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default)
        {
            return responseTask.AsApiRespAsync<TResult, TResult>(token);
        }

        public static async Task<TResult> AsApiRespAsync<TResult, TImpl>(this Task<HttpResponseMessage> responseTask, CancellationToken token = default) where TImpl : TResult
        {
            using var j = await responseTask.GetJsonAsync(token);
            var root = j.RootElement;
            if (root.CheckApiRespCode(out var code))
            {
                return root.Deserialize<TImpl>()!;
            }
            throw GetCommonException(code!.Value, in root);
        }
    }
}
