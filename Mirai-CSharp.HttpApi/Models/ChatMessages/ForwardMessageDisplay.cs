using System.Text.Json.Serialization;
using ISharedForwardMessageDisplay = Mirai.CSharp.Models.ChatMessages.IForwardMessageDisplay;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    public interface IForwardMessageDisplay : ISharedForwardMessageDisplay
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("title")]
        abstract string ISharedForwardMessageDisplay.Title { get; }
        
        [JsonPropertyName("brief")]
        abstract string ISharedForwardMessageDisplay.Brief { get; }
        
        [JsonPropertyName("source")]
        abstract string ISharedForwardMessageDisplay.Source { get; }
        
        [JsonPropertyName("preview")]
        abstract string[] ISharedForwardMessageDisplay.Preview { get; }
        
        [JsonPropertyName("summary")]
        abstract string ISharedForwardMessageDisplay.Summary { get; }
#else
        [JsonPropertyName("title")]
        new string Title { get; }

        [JsonPropertyName("brief")]
        new string Brief { get; }

        [JsonPropertyName("source")]
        new string Source { get; }

        [JsonPropertyName("preview")]
        new string[] Preview { get; }

        [JsonPropertyName("summary")]
        new string Summary { get; }
#endif
    }

    public class ForwardMessageDisplay : IForwardMessageDisplay
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("brief")]
        public string Brief { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("preview")]
        public string[] Preview { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        public ForwardMessageDisplay()
        {
            
        }

        public ForwardMessageDisplay(string title, string brief, string source, string[] preview, string summary)
        {
            Title = title;
            Brief = brief;
            Source = source;
            Preview = preview;
            Summary = summary;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("title")]
        string ISharedForwardMessageDisplay.Title => Title;

        [JsonPropertyName("brief")]
        string ISharedForwardMessageDisplay.Brief => Brief;

        [JsonPropertyName("source")]
        string ISharedForwardMessageDisplay.Source => Source;

        [JsonPropertyName("preview")]
        string[] ISharedForwardMessageDisplay.Preview => Preview;

        [JsonPropertyName("summary")]
        string ISharedForwardMessageDisplay.Summary => Summary;
#endif
    }
}
