using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1031 // Do not catch general exception types
namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 异步注册指令
        /// </summary>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <param name="name">指令名</param>
        /// <param name="alias">指令别名</param>
        /// <param name="description">指令描述</param>
        /// <param name="usage">指令用法, 会在指令执行错误时显示</param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static async Task RegisterCommandAsync(HttpClient client, MiraiHttpSessionOptions options, string name, string[]? alias = null, string? description = null, string? usage = null)
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
            string json = await client.PostAsJsonAsync($"{options.BaseUrl}/command/register", payload).GetStringAsync();
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                if (!root.CheckApiRespCode(out int? code))
                {
                    throw GetCommonException(code!.Value, in root);
                }
            }
            catch (JsonException) // 返回值非json就是执行失败, 把响应正文重新抛出
            {
                throw new InvalidOperationException(json);
            }
        }

        /// <inheritdoc cref="RegisterCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[], string, string)"/>
        public static Task RegisterCommandAsync(MiraiHttpSessionOptions options, string name, string[]? alias = null, string? description = null, string? usage = null)
        {
            return RegisterCommandAsync(_Client, options, name, alias, description, usage);
        }

        /// <inheritdoc cref="RegisterCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[], string, string)"/>
        public Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null)
        {
            InternalSessionInfo session = SafeGetSession();
            return RegisterCommandAsync(session.Client, session.Options, name, alias, description, usage);
        }

        /// <summary>
        /// 异步执行指令
        /// </summary>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <param name="name">指令名</param>
        /// <param name="args">指令参数</param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static async Task ExecuteCommandAsync(HttpClient client, MiraiHttpSessionOptions options, string name, params string[] args)
        {
            var payload = new
            {
                authKey = options.AuthKey,
                name,
                args
            };
            string json = await client.PostAsJsonAsync($"{options.BaseUrl}/command/send", payload).GetStringAsync();
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                if (!root.CheckApiRespCode(out int? code))
                {
                    throw GetCommonException(code!.Value, in root);
                }
            }
            catch (JsonException) // 返回值非json就是执行失败, 把响应正文重新抛出
            {
                throw new InvalidOperationException(json);
            }
            catch (TargetNotFoundException e) // 指令不存在
            {
                e._message = "给定的指令不存在。";
                throw;
            }
        }

        /// <inheritdoc cref="ExecuteCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[])"/>
        public static Task ExecuteCommandAsync(MiraiHttpSessionOptions options, string name, params string[] args)
        {
            return ExecuteCommandAsync(_Client, options, name, args);
        }

        /// <inheritdoc cref="ExecuteCommandAsync(HttpClient, MiraiHttpSessionOptions, string, string[])"/>
        public Task ExecuteCommandAsync(string name, params string[] args)
        {
            InternalSessionInfo session = SafeGetSession();
            return ExecuteCommandAsync(session.Options, name, args);
        }

        /// <summary>
        /// 异步获取给定QQ的Managers
        /// </summary>
        /// <exception cref="BotNotFoundException"/>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <param name="client">要进行请求的 <see cref="HttpClient"/></param>
        /// <param name="options">连接信息</param>
        /// <param name="qqNumber">机器人QQ号</param>
        /// <param name="token">用于取消操作的Token</param>
        /// <returns>能够管理此机器人的QQ号数组</returns>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        public static Task<long[]> GetManagersAsync(HttpClient client, MiraiHttpSessionOptions options, long qqNumber, CancellationToken token = default)
        {
            return client.GetAsync($"{options.BaseUrl}/managers?qq={qqNumber}", token).AsApiRespAsync<long[]>(token);
        }

        /// <inheritdoc cref="GetManagersAsync(HttpClient, MiraiHttpSessionOptions, long, CancellationToken)"/>
        public static Task<long[]> GetManagersAsync(MiraiHttpSessionOptions options, long qqNumber, CancellationToken token = default)
        {
            return GetManagersAsync(_Client, options, qqNumber, token);
        }

        /// <inheritdoc cref="GetManagersAsync(HttpClient, MiraiHttpSessionOptions, long, CancellationToken)"/>
        public Task<long[]> GetManagersAsync(long qqNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            return GetManagersAsync(session.Client, session.Options, qqNumber, session.Token);
        }
    }
}
