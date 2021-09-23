using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    /// <summary>
    /// 表示处理 <typeparamref name="TMessage"/> 类型消息或者比其派生程度更高的消息的接口
    /// </summary>
    /// <remarks>
    /// 如需处理固定为 <typeparamref name="TMessage"/> 的消息, 请使用 <see cref="IDedicateMiraiMessageHandler{TClient, TMessage}"/>
    /// </remarks>
    public interface IMiraiMessageHandler<in TClient, in TMessage> : IMiraiMessageHandlerBase<TClient, TMessage>, IContravarianceMiraiMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                                        where TMessage : IMiraiMessage
    {

    }

    public abstract class MiraiMessageHandler<TClient, TMessage> : MiraiMessageHandlerBase<TClient, TMessage>, IMiraiMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                       where TMessage : IMiraiMessage
    {

    }
}
