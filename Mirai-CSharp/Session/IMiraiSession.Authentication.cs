using Mirai_CSharp.Exceptions;
using Mirai_CSharp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial interface IMiraiSession
    {
        /// <remarks>
        /// 不会连接到指令监控的ws服务。此方法线程安全。但是在连接过程中, 如果尝试多次调用, 除了第一次以后的所有调用都将立即返回。
        /// </remarks>
        /// <inheritdoc cref="ConnectAsync(MiraiSessionOptions, long, bool)"/>
        Task ConnectAsync(MiraiSessionOptions options, long qqNumber);

        /// <summary>
        /// 异步连接到mirai-api-http。
        /// </summary>
        /// <remarks>
        /// 此方法线程安全。但是在连接过程中, 如果尝试多次调用, 除了第一次以后的所有调用都将立即返回。
        /// </remarks>
        /// <exception cref="BotNotFoundException"/>
        /// <exception cref="InvalidAuthKeyException"/>
        /// <param name="options">连接信息</param>
        /// <param name="qqNumber">Session将要绑定的Bot的qq号</param>
        /// <param name="listenCommand">是否监听指令相关的消息</param>
        Task ConnectAsync(MiraiSessionOptions options, long qqNumber, bool listenCommand);

        /// <summary>
        /// 异步获取mirai-api-http的版本号
        /// </summary>
        Task<Version> GetVersionAsync();

        /// <summary>
        /// 异步释放Session
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ReleaseAsync(CancellationToken token = default);
    }
}
