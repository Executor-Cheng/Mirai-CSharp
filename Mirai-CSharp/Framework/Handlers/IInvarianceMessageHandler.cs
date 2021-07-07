using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Framework.Handlers
{
    public interface IInvarianceMessageHandler<TMessage> : IMessageHandler<TMessage> where TMessage : IMessage
    {

    }
}
