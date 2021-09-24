using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Handlers
{
    public interface IInvarianceMiraiMessageHandler<TClient, TMessage> : IMiraiMessageHandlerBase<TClient, TMessage> where TClient : IMiraiSession
                                                                                                                     where TMessage : IMiraiMessage
    {

    }
}
