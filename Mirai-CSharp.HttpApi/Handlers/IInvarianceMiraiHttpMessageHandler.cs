using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.HttpApi.Handlers
{
    public interface IInvarianceMiraiHttpMessageHandler<TMessage> : IMiraiHttpMessageHandlerBase<TMessage>, IInvarianceMiraiMessageHandler<IMiraiHttpSession, TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
