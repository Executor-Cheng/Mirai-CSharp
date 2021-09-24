using Mirai.CSharp.Framework.Clients;
using Mirai.CSharp.Framework.Models.General;

namespace Mirai.CSharp.Framework.Handlers
{
    public interface IContravarianceMessageHandler<in TClient, in TMessage> : IMessageHandler<TClient, TMessage> where TClient : IMessageClient
                                                                                                                 where TMessage : IMessage
    {

    }
}
