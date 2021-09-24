using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.HttpApi.Session
{
    public partial interface IMiraiHttpSession : IMiraiSession
    {
        /// <summary>
        /// 会话连接状态 
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// 会话绑定的QQ号。未连接为 <see langword="null"/>。
        /// </summary>
        long? QQNumber { get; }

        /// <summary>
        /// 添加一个用于处理消息的 <see cref="IMiraiHttpMessageHandler"/>
        /// </summary>
        void AddPlugin(IMiraiHttpMessageHandler plugin);

        /// <summary>
        /// 移除一个用于处理消息的 <see cref="IMiraiHttpMessageHandler"/>。 <paramref name="plugin"/> 必须在之前通过 <see cref="AddPlugin(IMiraiHttpMessageHandler)"/> 添加过
        /// </summary>
        void RemovePlugin(IMiraiHttpMessageHandler plugin);
    }
}
