namespace Mirai_CSharp.Models
{
    public class MiraiHttpSessionOptions
    {
        /// <summary>
        /// 目标主机
        /// </summary>
        public string Host { get; }
        /// <summary>
        /// 目标端口
        /// </summary>
        public int Port { get; }
        /// <summary>
        /// 配置mirai-api-http时的AuthKey
        /// </summary>
        public string AuthKey { get; }
        /// <summary>
        /// 内部使用。
        /// </summary>
        internal string BaseUrl { get; }

        public MiraiHttpSessionOptions(string host, int port, string authKey)
        {
            Host = host;
            Port = port;
            AuthKey = authKey;
            BaseUrl = $"http://{host}:{port}";
        }
    }
}