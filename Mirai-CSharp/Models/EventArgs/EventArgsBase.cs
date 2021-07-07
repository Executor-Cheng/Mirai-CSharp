using Mirai_CSharp.Framework.Models.General;

namespace Mirai_CSharp.Models.EventArgs
{
    public interface IEventArgsBase : IMessage
    {

    }

    public abstract class EventArgsBase : Message, IEventArgsBase
    {

    }
}
