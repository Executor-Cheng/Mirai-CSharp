using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.HttpApi.Handlers
{
    /// <summary>
    /// 表示<strong>仅</strong>处理 <typeparamref name="TMessage"/> 类型消息的接口
    /// </summary>
    /// <remarks>
    /// 如需处理比 <typeparamref name="TMessage"/> 派生程度更高的消息, 请使用 <see cref="IMiraiHttpMessageHandler{TMessage}"/>
    /// </remarks>
    public interface IDedicateMiraiHttpMessageHandler<TMessage> : IMiraiHttpMessageHandlerBase<TMessage>, IDedicateMiraiMessageHandler<IMiraiHttpSession, TMessage>, IInvarianceMiraiHttpMessageHandler<TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    public abstract class DedicateMiraiHttpMessageHandler<TMessage> : MiraiHttpMessageHandlerBase<TMessage>, IDedicateMiraiHttpMessageHandler<TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
