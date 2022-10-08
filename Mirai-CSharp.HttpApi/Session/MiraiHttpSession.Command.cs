using Mirai.CSharp.Exceptions;
using Mirai.CSharp.Extensions;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Exceptions;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <inheritdoc cref="RegisterCommandAsync(string, string[], string, string, CancellationToken)"/>
        public static async Task RegisterCommandAsync(HttpClient client, MiraiHttpSessionOptions options, string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("指令名必须非空。", nameof(name));
            }
            var payload = new
            {
                authKey = options.AuthKey,
                name,
                alias = alias ?? Array.Empty<string>(),
                description,
                usage
            };
            string json = await client.PostAsJsonAsync($"{options.BaseUrl}/command/register", payload, token).GetStringAsync(token).ConfigureAwait(false);
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                root.EnsureApiRespCode();
            }
            catch (JsonException) // 返回值非json就是执行失败, 把响应正文重新抛出
            {
                throw new InvalidOperationException(json);
            }
        }

        /// <inheritdoc cref="RegisterCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[], string, string, CancellationToken)"/>
        public static Task RegisterCommandAsync(MiraiHttpSessionOptions options, string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default)
        {
            return RegisterCommandAsync(_globalClient, options, name, alias, description, usage, token);
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidAuthKeyException"/>
        public override Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return RegisterCommandAsync(_client, _options, name, alias, description, usage, token).DisposeWhenCompleted(cts);
        }

        /// <exception cref="InvalidAuthKeyException"/>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <inheritdoc cref="ExecuteCommandAsync(string, string[], CancellationToken)"/>
        public static async Task ExecuteCommandAsync(HttpClient client, MiraiHttpSessionOptions options, string name, string[]? args, CancellationToken token)
        {
            var payload = new
            {
                authKey = options.AuthKey,
                name,
                args = args ?? Array.Empty<string>()
            };
            string json = await client.PostAsJsonAsync($"{options.BaseUrl}/command/send", payload, token).GetStringAsync(token).ConfigureAwait(false);
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                root.EnsureApiRespCode();
            }
            catch (JsonException) // 返回值非json就是执行失败, 把响应正文重新抛出
            {
                throw new InvalidOperationException(json);
            }
            catch (TargetNotFoundException e) // 指令不存在
            {
                e.ActualMessage = "给定的指令不存在。";
                throw;
            }
        }

        /// <inheritdoc cref="ExecuteCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[], CancellationToken)"/>
        public static Task ExecuteCommandAsync(MiraiHttpSessionOptions options, string name, params string[] args)
        {
            return ExecuteCommandAsync(_globalClient, options, name, args, default);
        }

        /// <inheritdoc/>
        public override Task ExecuteCommandAsync(string name, string[]? args, CancellationToken token)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return ExecuteCommandAsync(_client, _options, name, args, token).DisposeWhenCompleted(cts);
        }

        /// <exception cref="InvalidAuthKeyException"/>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <inheritdoc cref="GetManagersAsync(long, CancellationToken)"/>
        [Obsolete("新版本的 mirai-console 中已经没有管理员概念了, 参考: https://github.com/project-mirai/mirai-api-http/pull/265#discussion_r598428011")]
        public static async Task<long[]> GetManagersAsync(HttpClient client, MiraiHttpSessionOptions options, long qqNumber, CancellationToken token = default)
        {
            string json = await client.GetAsync($"{options.BaseUrl}/managers?qq={qqNumber}", token).GetStringAsync(token).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<long[]>(json)!;
            }
            return Array.Empty<long>();
        }

        /// <inheritdoc cref="GetManagersAsync(HttpClient, MiraiHttpSessionOptions, long, CancellationToken)"/>
        [Obsolete("新版本的 mirai-console 中已经没有管理员概念了, 参考: https://github.com/project-mirai/mirai-api-http/pull/265#discussion_r598428011")]
        public static Task<long[]> GetManagersAsync(MiraiHttpSessionOptions options, long qqNumber, CancellationToken token = default)
        {
            return GetManagersAsync(_globalClient, options, qqNumber, token);
        }

        /// <inheritdoc/>
        [Obsolete("新版本的 mirai-console 中已经没有管理员概念了, 参考: https://github.com/project-mirai/mirai-api-http/pull/265#discussion_r598428011")]
        public override Task<long[]> GetManagersAsync(long qqNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return GetManagersAsync(_client, _options, qqNumber, token).DisposeWhenCompleted(cts);
        }
    }
}
