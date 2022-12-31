using Mirai.CSharp.HttpApi.Invoking;

namespace Mirai.CSharp.HttpApi.Options
{
    public class MiraiHttpSessionOptions
    {
        /// <summary>
        /// 目标主机
        /// </summary>
        public string Host { get; set; } = null!;
        /// <summary>
        /// 目标端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 配置mirai-api-http时的AuthKey
        /// </summary>
        public string AuthKey { get; set; } = null!;
        /// <summary>
        /// 指示不要异步等待消息被 <see cref="IMiraiHttpMessageHandlerInvoker"/> 处理完毕
        /// </summary>
        /// <remarks>
        /// 若此属性值为 <see langword="true"/>, 将不保证消息进入相应处理器的先后顺序
        /// </remarks>
        public bool SuppressAwaitMessageInvoker { get; set; }
        /// <summary>
        /// 请求形式
        /// </summary>
        /// <remarks>
        /// 如果本属性为 <see langword="null"/>, 将使用http
        /// </remarks>
        public string? Scheme { get; set; }
        /// <summary>
        /// 内部使用。
        /// </summary>
        internal string BaseUrl
        {
            get => _baseUrl ??= $"{Scheme ?? "http"}://{Host}:{Port}";
            set => _baseUrl = value;
        }

        private string? _baseUrl;

        public MiraiHttpSessionOptions() { }

        public MiraiHttpSessionOptions(string host, int port, string authKey)
        {
            Host = host;
            Port = port;
            AuthKey = authKey;
        }
    }
}
