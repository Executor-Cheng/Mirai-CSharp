using System;
using System.Threading.Tasks;
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Handlers;
using Mirai.CSharp.Framework.Models.General;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    /// <summary>
    /// 表示处理 <typeparamref name="TMessage"/> 类型消息的接口
    /// </summary>
    public interface IMiraiMessageHandlerBase<in TClient, in TMessage> : IMiraiMessageHandler, IMessageHandler<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                  where TMessage : IMiraiMessage
    {

    }

    public abstract class MiraiMessageHandlerBase<TClient, TMessage> : MessageHandler<TClient, TMessage>, IMiraiMessageHandlerBase<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                                                      where TMessage : IMiraiMessage
    {
        public override Task HandleMessageAsync(IMessageClient client, IMessage message)
        {
            return this.HandleMessageAsync((IMiraiSession)client, (IMiraiMessage)message);
        }

        public virtual Task HandleMessageAsync(IMiraiSession client, IMiraiMessage message)
        {
            throw new NotSupportedException("请使用泛型接口中的 HandleMessageAsync 方法。");
        }
    }
}
