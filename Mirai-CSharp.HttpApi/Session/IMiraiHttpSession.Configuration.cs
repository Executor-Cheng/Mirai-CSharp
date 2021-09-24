using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Options;
using System;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial interface IMiraiHttpSession
    {
        /// <summary>
        /// 异步获取当前会话的相应配置
        /// </summary>
        /// <remarks>
        /// 自 mirai-api-http v2.0.0 起, 本接口不受支持, 调用将抛出 <see cref="NotSupportedException"/>
        /// </remarks>
        /// <exception cref="NotSupportedException"/>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task<IMiraiSessionConfig> GetConfigAsync(CancellationToken token = default);

        /// <summary>
        /// 异步设置当前会话的Config
        /// </summary>
        /// <remarks>
        /// 自 mirai-api-http v2.0.0 起, 本接口不受支持, 调用将抛出 <see cref="NotSupportedException"/>
        /// </remarks>
        /// <exception cref="NotSupportedException"/>
        /// <param name="config">配置信息</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task SetConfigAsync(IMiraiSessionConfig config, CancellationToken token = default);
    }
}
