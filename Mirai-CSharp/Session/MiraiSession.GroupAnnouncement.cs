using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task<IGroupAnnouncement[]> GetGroupAnnouncementAsync(long groupNumber, int offset, int count, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task PublishGroupAnnouncementAsync(long groupNumber, IPublishGroupAnnouncementRequest request, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task DeleteGroupAnnouncementAsync(long groupNumber, string id, CancellationToken token = default);

        /// <inheritdoc/>
        public virtual Task DeleteGroupAnnouncementAsync(long groupNumber, IGroupAnnouncement announcement, CancellationToken token = default)
            => DeleteGroupAnnouncementAsync(groupNumber, announcement.Id, token);
    }
}
