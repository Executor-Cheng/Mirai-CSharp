using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Models.ChatMessages;

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

        /// <summary>
        /// 异步获取与给定好友的漫游聊天记录
        /// </summary>
        /// <remarks>
        /// 返回的 <see cref="IChatMessage"/>[][] 表示以消息链 <see cref="IChatMessage"/>[] 为类型的一个数组 <para/>
        /// 本 api 自 mirai-api-http v2.6.0 起可用
        /// </remarks>
        /// <param name="target">好友QQ号</param>
        /// <param name="from">指示从何时起的聊天记录</param>
        /// <param name="to">指示至何时的聊天记录</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task<IChatMessage[][]> GetChatHistoryAsync(long target, DateTime? from, DateTime? to, CancellationToken token = default);
    }
}
