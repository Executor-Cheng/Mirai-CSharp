using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
namespace Mirai.CSharp.Session
{
    public partial interface IMiraiSession
    {
        /// <summary>
        /// 异步获取给定群内的群公告
        /// </summary>
        /// <param name="groupNumber">要获取群公告的群号</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">欲获取的数目</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/>, 其值为群公告数组</returns>
        Task<IGroupAnnouncement[]> GetGroupAnnouncementAsync(long groupNumber, int offset, int count, CancellationToken token = default);

        /// <summary>
        /// 异步在给定群内发布群公告
        /// </summary>
        /// <param name="groupNumber">要发布群公告的群号</param>
        /// <param name="request">创建群公告请求实例</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task PublishGroupAnnouncementAsync(long groupNumber, IPublishGroupAnnouncementRequest request, CancellationToken token = default);

        /// <summary>
        /// 异步删除给定群内的给定群公告
        /// </summary>
        /// <param name="groupNumber">要删除群公告的群号</param>
        /// <param name="id">群公告Id</param>
        /// <param name="token">用于取消此异步操作的 <see cref="CancellationToken"/></param>
        /// <returns>表示此异步操作的 <see cref="Task"/></returns>
        Task DeleteGroupAnnouncementAsync(long groupNumber, string id, CancellationToken token = default);

        /// <inheritdoc cref="DeleteGroupAnnouncementAsync(long, long, CancellationToken)"/>
        /// <param name="announcement">群公告实例</param>
        Task DeleteGroupAnnouncementAsync(long groupNumber, IGroupAnnouncement announcement, CancellationToken token = default);
    }
}
