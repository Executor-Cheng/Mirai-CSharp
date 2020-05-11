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
        /// 配置mirai-api-http时的AccessKey
        /// </summary>
        public string AccessKey { get; }
        /// <summary>
        /// 内部使用。
        /// </summary>
        internal string BaseUrl { get; }

        public MiraiHttpSessionOptions(string host, int port, string accessKey)
        {
            Host = host;
            Port = port;
            AccessKey = accessKey;
            BaseUrl = $"http://{host}:{port}";
        }
    }
}