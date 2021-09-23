using System.Text.Json.Serialization;

namespace Mirai.CSharp.HttpApi.Options
{
    /// <summary>
    /// 提供 mirai-api-http 会话相关设置的接口
    /// </summary>
    public interface IMiraiSessionConfig
    {
        /// <summary>
        /// 缓存大小
        /// </summary>
        int? CacheSize { get; }
        /// <summary>
        /// 是否开启Websocket
        /// </summary>
        bool? EnableWebSocket { get; }
    }

    public class MiraiSessionConfig : IMiraiSessionConfig
    {
        /// <summary>
        /// 缓存大小
        /// </summary>
        public int? CacheSize { get; set; }
        /// <summary>
        /// 是否启用WebSocket
        /// </summary>
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
