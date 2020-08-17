using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Helpers;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="options">连接信息</param>
        /// <param name="name">指令名</param>
        /// <param name="alias">指令别名</param>
        /// <param name="description">指令描述</param>
        /// <param name="usage">指令用法, 会在指令执行错误时显示</param>
        public static async Task RegisterCommandAsync(MiraiHttpSessionOptions options, string name, string[]? alias = null, string? description = null, string? usage = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("指令名必须非空。", nameof(name));
            }
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                authKey = options.AuthKey,
                name,
                alias = alias ?? Array.Empty<string>(),
                description,
                usage
            });
            string json = await HttpHelper.HttpPostAsync($"{options.BaseUrl}/command/register", payload).GetStringAsync();
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                ProcessResponse(in root);
            }
            catch (JsonException) // 返回值非json就是执行失败, 把返回信息重新抛出
            {
                throw new InvalidOperationException(json);
            }
        }
        /// <summary>
        /// 异步注册指令
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="name">指令名</param>
        /// <param name="alias">指令别名</param>
        /// <param name="description">指令描述</param>
        /// <param name="usage">指令用法, 会在指令执行错误时显示</param>
        public Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null)
        {
            InternalSessionInfo session = SafeGetSession();
            return RegisterCommandAsync(session.Options, name, alias, description, usage);
        }
        /// <summary>
        /// 异步执行指令
        /// </summary>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="options">连接信息</param>
        /// <param name="name">指令名</param>
        /// <param name="args">指令参数</param>
        public static async Task ExecuteCommandAsync(MiraiHttpSessionOptions options, string name, params string[] args)
        {
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                authKey = options.AuthKey,
                name,
                args
            });
            string json = await HttpHelper.HttpPostAsync($"{options.BaseUrl}/command/send", payload).GetStringAsync();
            try
            {
                using JsonDocument j = JsonDocument.Parse(json);
                JsonElement root = j.RootElement;
                ProcessResponse(in root);
            }
            catch (JsonException) // 返回值非json就是执行失败, 把返回信息重新抛出
            {
                throw new InvalidOperationException(json);
            }
            catch (TargetNotFoundException e) // 指令不存在
            {
                e._message = "给定的指令不存在。";
                throw e;
            }
        }
        /// <summary>
        /// 异步执行指令
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="name">指令名</param>
        /// <param name="args">指令参数</param>
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
        /// <param name="options">连接信息</param>
        /// <param name="qqNumber">机器人QQ号</param>
        /// <param name="token">用于取消操作的Token</param>
        /// <returns>能够管理此机器人的QQ号数组</returns>
        public static Task<long[]> GetManagersAsync(MiraiHttpSessionOptions options, long qqNumber, CancellationToken token = default)
        {
            return InternalHttpGetNoSuccCodeAsync<long[], long[]>($"{options.BaseUrl}/managers?qq={qqNumber}", token);
        }
        /// <summary>
        /// 异步获取给定QQ的Managers
        /// </summary>
        /// <exception cref="BotNotFoundException"/>
        /// <param name="qqNumber">机器人QQ号</param>
        /// <returns>能够管理此机器人的QQ号数组</returns>
        public Task<long[]> GetManagersAsync(long qqNumber)
        {
            InternalSessionInfo session = SafeGetSession();
            return GetManagersAsync(session.Options, qqNumber, session.Token);
        }
    }
}
