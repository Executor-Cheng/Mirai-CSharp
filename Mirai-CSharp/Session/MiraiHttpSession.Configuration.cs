using Mirai_CSharp.Helpers;
using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        private async Task<IMiraiSessionConfig> GetConfigAsync(InternalSessionInfo session)
        {
            using JsonDocument j = await HttpHelper.HttpGetAsync($"{session.Options.BaseUrl}/config?sessionKey={WebUtility.UrlEncode(session.SessionKey)}").GetJsonAsync(token: session.Canceller.Token);
            JsonElement root = j.RootElement;
            return Utils.Deserialize<MiraiSessionConfig>(in root);
        }

        private Task SetConfigAsync(InternalSessionInfo session, IMiraiSessionConfig config)
        {
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = session.SessionKey,
                cacheSize = config.CacheSize,
                enableWebsocket = config.EnableWebSocket
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            return InternalHttpPostAsync($"{session.Options.BaseUrl}/config", payload, session.Canceller.Token);
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
