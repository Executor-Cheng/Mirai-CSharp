using System.Threading.Tasks;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.HttpApi.Handlers
{
    /// <summary>
    /// 本接口默认<strong>不</strong>处理任何的 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    /// <remarks>
    /// 一般情况下应当实现 <see cref="IMiraiHttpMessageHandler{TMessage}"/>
    /// </remarks>
    public interface IMiraiHttpMessageHandler : IMiraiMessageHandler
    {
#if !NETSTANDARD2_0
        Task HandleMessageAsync(IMiraiHttpSession client, IMiraiHttpMessage message)
        {
            return _DefaultImplTask;
        }

        Task IMiraiMessageHandler.HandleMessageAsync(IMiraiSession session, IMiraiMessage message)
        {
            return HandleMessageAsync((IMiraiHttpSession)session, (IMiraiHttpMessage)message);
        }
#else
        Task HandleMessageAsync(IMiraiHttpSession client, IMiraiHttpMessage message);
#endif
    }

    /// <summary>
    /// 表示处理 <typeparamref name="TMessage"/> 类型消息的接口
    /// </summary>
    public interface IMiraiHttpMessageHandlerBase<in TMessage> : IMiraiHttpMessageHandler, IMiraiMessageHandlerBase<IMiraiHttpSession, TMessage> where TMessage : IMiraiHttpMessage
    {

    }

    public abstract class MiraiHttpMessageHandlerBase<TMessage> : MiraiMessageHandlerBase<IMiraiHttpSession, TMessage>, IMiraiHttpMessageHandlerBase<TMessage> where TMessage : IMiraiHttpMessage
    {
        public override Task HandleMessageAsync(IMiraiSession client, IMiraiMessage message)
        {
            return HandleMessageAsync((IMiraiHttpSession)client, (IMiraiHttpMessage)message);
        }

#if NETSTANDARD2_0
        public virtual Task HandleMessageAsync(IMiraiHttpSession client, IMiraiHttpMessage message)
        {
            return Task.FromException(new System.NotSupportedException("请使用泛型接口中的HandleMessageAsync方法。"));
        }
#endif
    }
}
