using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Models;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial interface IMiraiHttpSession
    {
        /// <summary>
        /// 异步获取缓存于 mirai-api-http 的消息
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        /// <param name="messageId">消息Id</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        [Obsolete("自mirai-api-http 2.6.0起, 要求传入消息所在的群号或好友QQ号, 请考虑调用RetriveMessageAsync(int, long, CancellationToken)方法")]
        Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, CancellationToken token = default);

        /// <summary>
        /// 异步获取缓存于 mirai-api-http 的消息
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        /// <param name="messageId">消息Id</param>
        /// <param name="target">消息来源群号/好友QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, long target, CancellationToken token = default);
    }
}
