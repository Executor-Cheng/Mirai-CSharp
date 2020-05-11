using Mirai_CSharp.Models;
using Mirai_CSharp.Utility;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mirai_CSharp
{
    public partial class MiraiHttpSession
    {
        /// <summary>
        /// 异步获取当前Session的Config
        /// </summary>
        public Task<IMiraiSessionConfig> GetConfigAsync()
        {
            CheckConnected();
            return InternalHttpGetAsync<IMiraiSessionConfig, MiraiSessionConfig>($"{SessionInfo.Options.BaseUrl}/config?sessionKey={WebUtility.UrlEncode(SessionInfo.SessionKey)}", SessionInfo.Canceller.Token);
        }
        /// <summary>
        /// 异步设置当前Session的Config
        /// </summary>
        /// <param name="config">配置信息</param>
        public Task SetConfigAsync(IMiraiSessionConfig config)
        {
            CheckConnected();
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                sessionKey = SessionInfo.SessionKey,
                cacheSize = config.CacheSize,
                enableWebsocket = config.EnableWebSocket
            }, JsonSerializeOptionsFactory.IgnoreNulls);
            return InternalHttpPostAsync($"{SessionInfo.Options.BaseUrl}/config", payload, SessionInfo.Canceller.Token);
        }
    }
}
