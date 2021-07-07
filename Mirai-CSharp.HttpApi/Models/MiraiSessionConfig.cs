using System.Text.Json.Serialization;

namespace Mirai_CSharp.Models
{
    public interface IMiraiSessionConfig
    {
        /// <summary>
        /// 缓存大小
        /// </summary>
        [JsonPropertyName("cacheSize")]
        int? CacheSize { get; }
        /// <summary>
        /// 是否开启Websocket
        /// </summary>
        [JsonPropertyName("enableWebsocket")]
        bool? EnableWebSocket { get; }
    }

    public class MiraiSessionConfig : IMiraiSessionConfig
    {
        /// <summary>
        /// 缓存大小
        /// </summary>
        [JsonPropertyName("cacheSize")]
        public int? CacheSize { get; set; }
        /// <summary>
        /// 是否启用WebSocket
        /// </summary>
        [JsonPropertyName("enableWebsocket")]
        public bool? EnableWebSocket { get; set; }

        public MiraiSessionConfig()
        {

        }

        public MiraiSessionConfig(int? cacheSize, bool? enableWebSocket)
        {
            CacheSize = cacheSize;
            EnableWebSocket = enableWebSocket;
        }
    }
}
