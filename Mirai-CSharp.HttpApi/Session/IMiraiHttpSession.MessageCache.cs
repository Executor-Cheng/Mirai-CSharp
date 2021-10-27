using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Parsers;

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
        Task<IMiraiHttpMessage?> RetriveMessageAsync(int messageId, CancellationToken token = default);
    }
}
