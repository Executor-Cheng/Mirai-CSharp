using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Framework.Handlers
{
    public interface IContravarianceMessageHandler<in TMessage> : IMessageHandler<TMessage> where TMessage : IMessage
    {

    }
}
