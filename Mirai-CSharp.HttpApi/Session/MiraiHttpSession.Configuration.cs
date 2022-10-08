using Mirai.CSharp.Extensions;
using Mirai.CSharp.HttpApi.Extensions;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Utility;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System;
#if NET5_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Mirai.CSharp.HttpApi.Session
{
    public partial class MiraiHttpSession
    {
        private Task<IMiraiSessionConfig> GetConfigAsync(InternalSessionInfo session, CancellationToken token = default)
        {
            return _client.GetAsync($"{_options.BaseUrl}/config?sessionKey={WebUtility.UrlEncode(session.SessionKey)}", token)
                .AsApiRespAsync<IMiraiSessionConfig, MiraiSessionConfig>(token);
        }

        private Task SetConfigAsync(InternalSessionInfo session, IMiraiSessionConfig config, CancellationToken token = default)
        {
            var payload = new
            {
                sessionKey = session.SessionKey,
                cacheSize = config.CacheSize,
                enableWebsocket = config.EnableWebSocket
            };
            return _client.PostAsJsonAsync($"{_options.BaseUrl}/config", payload, JsonSerializeOptionsFactory.IgnoreNulls, token).AsApiRespAsync(token);
        }

        /// <inheritdoc/>
        public Task<IMiraiSessionConfig> GetConfigAsync(CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            if (session.ApiVersion.Major >= 2)
            {
                throw new NotSupportedException("自 mirai-api-http v2.0.0 起, 本接口不受支持");
            }
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return GetConfigAsync(session, token).DisposeWhenCompleted(cts);
        }

        /// <inheritdoc/>
        public Task SetConfigAsync(IMiraiSessionConfig config, CancellationToken token = default)
        {
            InternalSessionInfo session = SafeGetSession();
            if (session.ApiVersion.Major >= 2)
            {
                throw new NotSupportedException("自 mirai-api-http v2.0.0 起, 本接口不受支持");
            }
            CreateLinkedUserSessionToken(session.Token, token, out CancellationTokenSource? cts, out token);
            return SetConfigAsync(session, config, token).DisposeWhenCompleted(cts);
        }
    }
}
