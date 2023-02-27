namespace Mirai.CSharp.Models.ChatMessages
{
    public interface IForwardMessageNodeReference
    {
        int MessageId { get; }

        long Target { get; }
    }
}
