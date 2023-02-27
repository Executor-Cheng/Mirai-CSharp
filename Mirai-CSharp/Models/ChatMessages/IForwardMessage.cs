namespace Mirai.CSharp.Models.ChatMessages
{
    public interface IForwardMessage : IChatMessage
    {
        IForwardMessageNode[] Nodes { get; }

        IForwardMessageDisplay? Display { get; }
    }
}
