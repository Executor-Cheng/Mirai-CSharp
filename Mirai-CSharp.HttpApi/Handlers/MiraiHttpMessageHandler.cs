using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.HttpApi.Handlers
{
    /// <summary>
    /// 表示处理 <typeparamref name="TMessage"/> 类型消息或者比其派生程度更高的消息的接口
    /// </summary>
    /// <remarks>
    /// 如需处理固定为 <typeparamref name="TMessage"/> 的消息, 请使用 <see cref="IDedicateMiraiHttpMessageHandler{TMessage}"/>
    /// </remarks>
    public interface IMiraiHttpMessageHandler<in TMessage> : IMiraiHttpMessageHandlerBase<TMessage>, IMiraiMessageHandler<IMiraiHttpSession, TMessage>, IContravarianceMiraiHttpMessageHandler<TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    public abstract class MiraiHttpMessageHandler<TMessage> : MiraiHttpMessageHandlerBase<TMessage>, IMiraiHttpMessageHandler<TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
