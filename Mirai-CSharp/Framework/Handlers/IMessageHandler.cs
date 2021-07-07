using System;
using System.Threading.Tasks;
using Mirai_CSharp.Framework.Clients;
using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Framework.Handlers
{
    public interface IMessageHandler
    {
        protected static readonly Task _DefaultImplTask = Task.FromException(new NotSupportedException("请使用泛型接口中的HandleMessageAsync方法。"));

        Task HandleMessageAsync(IMessageClient client, IMessage message)
            => _DefaultImplTask;
    }

    public interface IMessageHandler<in TMessage> : IMessageHandler where TMessage : IMessage
    {
        Task HandleMessageAsync(IMessageClient client, TMessage message);
    }
}
