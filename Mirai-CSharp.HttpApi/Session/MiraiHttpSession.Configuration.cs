using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System.Net;
using System.Threading.Tasks;
#if NET5_0
using System.Net.Http.Json;
#endif

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private Task<IMiraiSessionConfig> GetConfigAsync(InternalSessionInfo session)
        {
            return session.Client.GetAsync($"{session.Options.BaseUrl}/config?sessionKey={WebUtility.UrlEncode(session.SessionKey)}", session.Token)
                .AsApiRespAsync<IMiraiSessionConfig, MiraiSessionConfig>(session.Token);
        }

        private Task SetConfigAsync(InternalSessionInfo session, IMiraiSessionConfig config)
        {
            var payload = new
            {
                sessionKey = session.SessionKey,
                cacheSize = config.CacheSize,
                enableWebsocket = config.EnableWebSocket
            };
            return session.Client.PostAsJsonAsync($"{session.Options.BaseUrl}/config", payload, JsonSerializeOptionsFactory.IgnoreNulls, session.Token).AsApiRespAsync(session.Token);
        }

        /// <summary>
        /// 异步获取当前Session的Config
        /// </summary>
        public Task<IMiraiSessionConfig> GetConfigAsync()
        {
            InternalSessionInfo session = SafeGetSession();
            return GetConfigAsync(session);
        }

        /// <summary>
        /// 异步设置当前Session的Config
        /// </summary>
        /// <param name="config">配置信息</param>
        public Task SetConfigAsync(IMiraiSessionConfig config)
        {
            InternalSessionInfo session = SafeGetSession();
            return SetConfigAsync(session, config);
        }
    }
}
