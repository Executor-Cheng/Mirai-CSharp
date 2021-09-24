using Mirai.CSharp.Framework.Models.General;

namespace Mirai.CSharp.Models.EventArgs
{
    public interface IMiraiMessage : IMessage
    {
        
    }

    public interface IMiraiMessage<TRawdata> : IMiraiMessage, IMessage<TRawdata>
    {

    }

    public abstract class MiraiMessage<TRawdata> : Message<TRawdata>, IMiraiMessage<TRawdata>
    {

    }
}
