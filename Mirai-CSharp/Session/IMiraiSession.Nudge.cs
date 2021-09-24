using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步发送头像戳一戳消息
        /// </summary>
        /// <param name="target">要戳的对象</param>
        /// <param name="qqNumber">要戳的QQ号 (可以是Bot自己)</param>
        /// <param name="groupNumber">要戳的对象所在的群号 (戳 <see cref="NudgeTarget.Friend"/> / <see cref="NudgeTarget.Stranger"/> 时可以为 <see langword="null"/>)</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task NudgeAsync(NudgeTarget target, long qqNumber, long? groupNumber = null, CancellationToken token = default);
    }
}
