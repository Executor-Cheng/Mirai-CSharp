using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    /// <summary>
    /// 表示<strong>仅</strong>处理 <typeparamref name="TMessage"/> 类型消息的接口
    /// </summary>
    /// <remarks>
    /// 如需处理比 <typeparamref name="TMessage"/> 派生程度更高的消息, 请使用 <see cref="IMiraiMessageHandler{TClient, TMessage}"/>
    /// </remarks>
    public interface IDedicateMiraiMessageHandler<TClient, TMessage> : IMiraiMessageHandlerBase<TClient, TMessage>, IInvarianceMiraiMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                                      where TMessage : IMiraiMessage
    {

    }

    public abstract class DedicateMiraiMessageHandler<TClient, TMessage> : MiraiMessageHandlerBase<TClient, TMessage>, IDedicateMiraiMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                                       where TMessage : IMiraiMessage
    {

    }
}
