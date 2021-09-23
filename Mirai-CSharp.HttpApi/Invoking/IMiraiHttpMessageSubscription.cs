using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Invoking;
#if !NETSTANDARD2_0
using System.Threading.Tasks;
#endif

namespace Mirai.CSharp.HttpApi.Invoking
{
    public interface IMiraiHttpMessageSubscription : IMiraiMessageSubscription, IMiraiHttpMessageHandler
    {

    }

    public interface IMiraiHttpMessageSubscription<TMessage> : IMiraiHttpMessageSubscription,
                                                               IMiraiMessageSubscription<IMiraiHttpSession, TMessage>,
                                                               IMiraiHttpMessageHandler<TMessage> where TMessage : IMiraiHttpMessage
    {
#if !NETSTANDARD2_0
        Task IMiraiHttpMessageHandler.HandleMessageAsync(IMiraiHttpSession client, IMiraiHttpMessage message)
            => HandleMessageAsync(client, (TMessage)message);
#endif
    }
}
