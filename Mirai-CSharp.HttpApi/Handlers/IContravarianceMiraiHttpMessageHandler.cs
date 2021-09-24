using Mirai.CSharp.Handlers;
using Mirai.CSharp.HttpApi.Models;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.HttpApi.Handlers
{
    public interface IContravarianceMiraiHttpMessageHandler<in TMessage> : IMiraiHttpMessageHandlerBase<TMessage>, IContravarianceMiraiMessageHandler<IMiraiHttpSession, TMessage> where TMessage : IMiraiHttpMessage
    {

    }
}
