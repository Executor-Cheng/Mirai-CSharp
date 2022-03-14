using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using ISharedBotProfile = Mirai.CSharp.Models.IBotProfile;
using ISharedUserProfile = Mirai.CSharp.Models.IUserProfile;
using ISharedFriendProfile = Mirai.CSharp.Models.IFriendProfile;
using ISharedGroupMemberProfile = Mirai.CSharp.Models.IGroupMemberProfile;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task<ISharedBotProfile> GetBotProfileAsync(CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/botProfile?sessionKey={session.SessionKey}", token)
                .AsApiRespAsync<ISharedBotProfile, BotProfile>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedFriendProfile> GetFriendProfileAsync(long qqNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/friendProfile?sessionKey={session.SessionKey}&target={qqNumber}", token)
                .AsApiRespAsync<ISharedFriendProfile, FriendProfile>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedUserProfile> GetUserProfileAsync(long qqNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            if (session.ApiVersion < new System.Version(2, 4))
            {
                throw new System.NotSupportedException("本接口仅在 mirai-api-http v2.4.0 及以上版本提供");
            }
            return _client.GetAsync($"{_options.BaseUrl}/userProfile?sessionKey={session.SessionKey}&userId={qqNumber}", token)
                .AsApiRespAsync<ISharedUserProfile, UserProfile>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupMemberProfile> GetGroupMemberProfileAsync(long groupMember, long qqNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/memberProfile?sessionKey={session.SessionKey}&target={groupMember}&memberId={qqNumber}", token)
                .AsApiRespAsync<ISharedGroupMemberProfile, GroupMemberProfile>(token)
                .DisposeWhenCompleted(cts);
        }
    }
}
