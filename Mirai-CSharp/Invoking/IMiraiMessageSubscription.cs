#if !NETSTANDARD2_0
using System.Threading.Tasks;
#endif
using Mirai.CSharp.Framework.Invoking;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Invoking
{
    public interface IMiraiMessageSubscription : IMessageSubscription, IMiraiMessageHandler
    {

    }

    public interface IMiraiMessageSubscription<TClient, TMessage> : IMiraiMessageSubscription,
                                                                    IMessageSubscription<TClient, TMessage>,
                                                                    IMiraiMessageHandler<TClient, TMessage> where TClient : IMiraiSession // 只允许实现一种泛型接口
                                                                                                            where TMessage : IMiraiMessage // 想实现多种的请自己解决CS8705
    {
#if !NETSTANDARD2_0
        Task IMiraiMessageHandler.HandleMessageAsync(IMiraiSession client, IMiraiMessage message)
            => HandleMessageAsync((TClient)client, (TMessage)message);
#endif
    }
}
