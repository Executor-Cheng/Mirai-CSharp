using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Exceptions;

namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步执行指令
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <exception cref="TargetNotFoundException"/>
        /// <param name="name">指令名</param>
        /// <param name="args">指令参数</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task ExecuteCommandAsync(string name, string[]? args, CancellationToken token = default);

        /// <summary>
        /// 异步注册指令
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="name">指令名</param>
        /// <param name="alias">指令别名</param>
        /// <param name="description">指令描述</param>
        /// <param name="usage">指令用法, 会在指令执行错误时显示</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default);

        /// <summary>
        /// 异步获取给定QQ的管理员QQ
        /// </summary>
        /// <exception cref="BotNotFoundException"/>
        /// <param name="qqNumber">机器人QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/>, 其值为能够管理此机器人的QQ号数组</returns>
        [Obsolete("新版本的 mirai-console 中已经没有管理员概念了, 参考: https://github.com/project-mirai/mirai-api-http/pull/265#discussion_r598428011")]
        Task<long[]> GetManagersAsync(long qqNumber, CancellationToken token = default);
    }
}
