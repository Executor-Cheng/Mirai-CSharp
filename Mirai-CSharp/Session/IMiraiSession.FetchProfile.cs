using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步获取当前会话机器人的资料
        /// </summary>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>当前会话机器人的资料</returns>
        Task<IBotProfile> GetBotProfileAsync(CancellationToken token = default);

        /// <summary>
        /// 异步获取给定QQ好友的资料
        /// </summary>
        /// <param name="qqNumber">好友QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>给定QQ好友的资料</returns>
        Task<IFriendProfile> GetFriendProfileAsync(long qqNumber, CancellationToken token = default);

        /// <summary>
        /// 异步获取给定QQ的资料
        /// </summary>
        /// <param name="qqNumber">QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>给定QQ的资料</returns>
        Task<IUserProfile> GetUserProfileAsync(long qqNumber, CancellationToken token = default);

        /// <summary>
        /// 异步获取给定群员的资料
        /// </summary>
        /// <param name="groupMember">群员所在群号</param>
        /// <param name="qqNumber">群员QQ号</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>给定群员的资料</returns>
        Task<IGroupMemberProfile> GetGroupMemberProfileAsync(long groupMember, long qqNumber, CancellationToken token = default);
    }
}
