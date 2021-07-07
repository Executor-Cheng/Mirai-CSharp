using System.Threading.Tasks;
using Mirai_CSharp.Framework.Clients;
using Mirai_CSharp.Framework.Handlers;
using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Framework.Invoking
{
    public interface IMessageSubscription : IMessageHandler
    {
        abstract Task IMessageHandler.HandleMessageAsync(IMessageClient client, IMessage message); // re-abstract
    }

    public interface IMessageSubscription<TMessage> : IMessageSubscription, IMessageHandler<TMessage> where TMessage : IMessage // 只允许实现一种泛型接口
                                                                                                                                // 想实现多种的请自己解决CS8705
    {
        Task IMessageHandler.HandleMessageAsync(IMessageClient client, IMessage message)
            => HandleMessageAsync(client, (TMessage)message);
    }
}
