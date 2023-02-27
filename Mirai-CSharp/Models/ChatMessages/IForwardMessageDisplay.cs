namespace Mirai.CSharp.Models.ChatMessages
{
    public interface IForwardMessageDisplay
    {
        string Title { get; }

        string Brief { get; }

        string Source { get; }

        string[] Preview { get; }

        string Summary { get; }
    }
}
