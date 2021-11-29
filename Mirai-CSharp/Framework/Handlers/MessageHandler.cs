using System;
using System.Threading.Tasks;
using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Models.General;

namespace Mirai.CSharp.Framework.Handlers
{
    public interface IMessageHandler
    {
#if !NETSTANDARD2_0
        Task HandleMessageAsync(IMessageClient client, IMessage message)
        {
            throw new NotSupportedException("请使用泛型接口中的 HandleMessageAsync 方法。");
        }
#else
        Task HandleMessageAsync(IMessageClient client, IMessage message);
#endif
    }

    public interface IMessageHandler<in TClient, in TMessage> : IMessageHandler where TClient : IMessageClient
                                                                                where TMessage : IMessage
    {
        Task HandleMessageAsync(TClient client, TMessage message);
    }

    public abstract class MessageHandler<TClient, TMessage> : IMessageHandler<TClient, TMessage> where TClient : IMessageClient
                                                                                                 where TMessage : IMessage
    {
        public abstract Task HandleMessageAsync(TClient client, TMessage message);

        public virtual Task HandleMessageAsync(IMessageClient client, IMessage message)
        {
            throw new NotSupportedException("请使用泛型接口中的 HandleMessageAsync 方法。");
        }
    }
}
