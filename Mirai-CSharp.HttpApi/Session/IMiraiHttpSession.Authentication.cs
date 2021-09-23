using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Exceptions;
using Mirai.CSharp.HttpApi.Exceptions;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial interface IMiraiHttpSession
    {
        /// <remarks>
        /// 不会连接到指令监控的ws服务, 除非使用的 mirai-api-http版本 >= 2.0。此方法线程安全。如在连接过程中或已连接时尝试调用都将抛出 <see cref="InvalidOperationException"/>
        /// </remarks>
        /// <inheritdoc cref="ConnectAsync(long, bool, CancellationToken)"/>
        Task ConnectAsync(long qqNumber, CancellationToken token = default);

        /// <summary>
        /// 异步连接到 mirai-api-http
        /// </summary>
        /// <remarks>
        /// 此方法线程安全。如在连接过程中或已连接时尝试调用都将抛出 <see cref="InvalidOperationException"/>
        /// </remarks>
        /// <exception cref="BotNotFoundException"/>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="qqNumber">会话将要绑定的机器人QQ号</param>
        /// <param name="listenCommand">是否监听指令相关的消息。若使用的 mirai-api-http版本 >= 2.0, 本参数固定为 <see langword="true"/></param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        Task ConnectAsync(long qqNumber, bool listenCommand, CancellationToken token = default);

        /// <summary>
        /// 异步获取 mirai-api-http 的版本号
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task<Version> GetVersionAsync(CancellationToken token = default);

        /// <summary>
        /// 异步释放当前会话
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task ReleaseAsync(CancellationToken token = default);
    }
}
