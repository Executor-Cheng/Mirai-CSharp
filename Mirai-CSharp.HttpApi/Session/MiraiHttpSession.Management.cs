using System;
using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Utility;
using ISharedFriendInfo = Mirai.CSharp.Models.IFriendInfo;
using ISharedGroupConfig = Mirai.CSharp.Models.IGroupConfig;
using ISharedGroupInfo = Mirai.CSharp.Models.IGroupInfo;
using ISharedGroupMemberCardInfo = Mirai.CSharp.Models.IGroupMemberCardInfo;
using ISharedGroupMemberInfo = Mirai.CSharp.Models.IGroupMemberInfo;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        /// <inheritdoc/>
        public override Task<ISharedFriendInfo[]> GetFriendListAsync(CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/friendList?sessionKey={session.SessionKey}", token)
                .AsApiRespAsync<ISharedFriendInfo[], FriendInfo[]>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupInfo[]> GetGroupListAsync(CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/groupList?sessionKey={session.SessionKey}", token)
                .AsApiRespAsync<ISharedGroupInfo[], GroupInfo[]>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupMemberInfo[]> GetGroupMemberListAsync(long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/memberList?sessionKey={session.SessionKey}&target={groupNumber}", token)
                .AsApiRespAsync<ISharedGroupMemberInfo[], GroupMemberInfo[]>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <summary>
        /// 内部使用
        /// </summary>
        private Task InternalToggleMuteAllAsync(bool action, long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/{(action ? "muteAll" : "unmuteAll")}", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task MuteAllAsync(long groupNumber, CancellationToken token = default)
        {
            return InternalToggleMuteAllAsync(true, groupNumber, token);
        }

        /// <inheritdoc/>
        public override Task UnmuteAllAsync(long groupNumber, CancellationToken token = default)
        {
            return InternalToggleMuteAllAsync(false, groupNumber, token);
        }

        /// <inheritdoc/>
        public override Task MuteAsync(long memberId, long groupNumber, TimeSpan duration, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            if (duration <= TimeSpan.Zero || duration >= TimeSpan.FromDays(30))
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                time = (int)duration.TotalSeconds
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/mute", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task UnmuteAsync(long memberId, long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/unmute", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task KickMemberAsync(long memberId, long groupNumber, string msg = "您已被移出群聊", CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                msg
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/kick", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task LeaveGroupAsync(long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/quit", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task DeleteFriendAsync(long qqNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = qqNumber,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/deleteFriend", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task SetEssenceMessageAsync(int id, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = id,
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/setEssence", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task ChangeGroupConfigAsync(long groupNumber, ISharedGroupConfig config, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                config
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/groupConfig", payload, JsonSerializeOptionsFactory.IgnoreNulls, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupConfig> GetGroupConfigAsync(long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/groupConfig?sessionKey={session.SessionKey}&target={groupNumber}", token)
                .AsApiRespAsync<ISharedGroupConfig, GroupConfig>(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task ChangeGroupMemberInfoAsync(long memberId, long groupNumber, ISharedGroupMemberCardInfo info, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            var payload = new
            {
                sessionKey = session.SessionKey,
                target = groupNumber,
                memberId,
                info
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/memberInfo", payload, token)
                .AsApiRespAsync(token)
                .DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public override Task<ISharedGroupMemberCardInfo> GetGroupMemberInfoAsync(long memberId, long groupNumber, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return _client.GetAsync($"{_options.BaseUrl}/memberInfo?sessionKey={session.SessionKey}&target={groupNumber}&memberId={memberId}", token)
                .AsApiRespAsync<ISharedGroupMemberCardInfo, GroupMemberCardInfo>(token)
                .DisposeWhenCompleted(cts);
        }
    }
}
