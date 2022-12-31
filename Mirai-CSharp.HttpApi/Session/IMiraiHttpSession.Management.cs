using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial interface IMiraiHttpSession
    {
        /// <summary>
        /// 异步获取已登录的所有QQ号
        /// </summary>
        /// <remarks>
        /// 本 api 自 mirai-api-http v2.6.0 起可用
        /// </remarks>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task<long[]> GetBotListAsync(CancellationToken token = default);
    }
}
