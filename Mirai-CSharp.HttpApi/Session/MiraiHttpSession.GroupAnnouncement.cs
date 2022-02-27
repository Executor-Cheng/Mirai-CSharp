using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using ISharedGroupAnnouncement = Mirai.CSharp.Models.IGroupAnnouncement;
using ISharedPublishGroupAnnouncementRequest = Mirai.CSharp.Models.IPublishGroupAnnouncementRequest;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task<ISharedGroupAnnouncement[]> GetGroupAnnouncementAsync(long groupNumber, int offset, int count, CancellationToken token = default)
        {
            if (offset <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "无效的偏移量。必须大于0");
            }
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "无效的获取数目。必须大于0");
            }
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = groupNumber,
                offset,
                count
            };
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/anno/list", payload, token).AsApiRespAsync<ISharedGroupAnnouncement[], GroupAnnouncement[]>(token).DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task PublishGroupAnnouncementAsync(long groupNumber, ISharedPublishGroupAnnouncementRequest request, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(request.Content))
            {
                throw new ArgumentException("群公告内容不能为空");
            }
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            if (request is not PublishGroupAnnouncementRequest payload)
            {
                payload = new PublishGroupAnnouncementRequest(
                    request.GroupNumber,
                    request.Content,
                    request.SendToNewMember,
                    request.Pinned,
                    request.ShowEditMemberCard,
                    request.AutoPopup,
                    request.RequireConfirmation,
                    request.ImageUrl,
                    request.ImagePath,
                    request.ImageBase64);
            }
            payload.SessionKey = session.SessionKey;
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/anno/publish", payload, token).AsApiRespAsync(token).DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task DeleteGroupAnnouncementAsync(long groupNumber, string id, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            var payload = new
            {
                sessionKey = session.SessionKey,
                id = groupNumber,
                fid = id
            };
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/anno/delete", payload, token).AsApiRespAsync(token).DisposeWhenCompleted(cts);
        }
    }
}
