using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedSharedMusic = Mirai.CSharp.Models.ChatMessages.ISharedMusicMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedSharedMusic"/>
    [MappableMiraiChatMessageKey(SharedMusicMessage.MsgType)]
    public interface ISharedMusicMessage : ISharedSharedMusic, IChatMessage
    {
#if !NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("kind")]
        abstract string ISharedSharedMusic.Kind { get; }

        /// <inheritdoc/>
        [JsonPropertyName("title")]
        abstract string ISharedSharedMusic.Title { get; }

        /// <inheritdoc/>
        [JsonPropertyName("summary")]
        abstract string ISharedSharedMusic.Summary { get; }

        /// <inheritdoc/>
        [JsonPropertyName("jumpUrl")]
        abstract string ISharedSharedMusic.JumpUrl { get; }

        /// <inheritdoc/>
        [JsonPropertyName("pictureUrl")]
        abstract string ISharedSharedMusic.PictureUrl { get; }

        /// <inheritdoc/>
        [JsonPropertyName("musicUrl")]
        abstract string ISharedSharedMusic.MusicUrl { get; }

        /// <inheritdoc/>
        [JsonPropertyName("brief")]
        abstract string ISharedSharedMusic.Brief { get; }
#else
        /// <inheritdoc cref="ISharedSharedMusic.Kind"/>
        [JsonPropertyName("kind")]
        new string Kind { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.Title"/>
        [JsonPropertyName("title")]
        new string Title { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.Summary"/>
        [JsonPropertyName("summary")]
        new string Summary { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.JumpUrl"/>
        [JsonPropertyName("jumpUrl")]
        new string JumpUrl { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.PictureUrl"/>
        [JsonPropertyName("pictureUrl")]
        new string PictureUrl { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.MusicUrl"/>
        [JsonPropertyName("musicUrl")]
        new string MusicUrl { get; }
        
        /// <inheritdoc cref="ISharedSharedMusic.Brief"/>
        [JsonPropertyName("brief")]
        new string Brief { get; }
#endif
    }

    public class SharedMusicMessage : ChatMessage, ISharedMusicMessage
    {
        public const string MsgType = "MusicShare";

        /// <inheritdoc/>
        [JsonPropertyName("type")]
        public override string Type => MsgType;

        [JsonPropertyName("kind")]
        public string Kind { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = null!;

        [JsonPropertyName("jumpUrl")]
        public string JumpUrl { get; set; } = null!;

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; } = null!;

        [JsonPropertyName("musicUrl")]
        public string MusicUrl { get; set; } = null!;

        [JsonPropertyName("brief")]
        public string Brief { get; set; } = null!;

        /// <inheritdoc cref="SharedMusicMessage(string, string, string, string, string, string, string)"/>
        public SharedMusicMessage()
        {

        }

        /// <summary>
        /// 初始化 <see cref="SharedMusicMessage"/> 类的新实例
        /// </summary>
        /// <param name="kind">类型</param>
        /// <param name="title">标题</param>
        /// <param name="summary">简述</param>
        /// <param name="jumpUrl">跳转链接</param>
        /// <param name="pictureUrl">封面图片链接</param>
        /// <param name="musicUrl">音源链接</param>
        /// <param name="brief">简介</param>
        public SharedMusicMessage(string kind, string title, string summary, string jumpUrl, string pictureUrl, string musicUrl, string brief)
        {
            Kind = kind;
            Title = title;
            Summary = summary;
            JumpUrl = jumpUrl;
            PictureUrl = pictureUrl;
            MusicUrl = musicUrl;
            Brief = brief;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("kind")]
        string ISharedSharedMusic.Kind => Kind;

        /// <inheritdoc/>
        [JsonPropertyName("title")]
        string ISharedSharedMusic.Title => Title;

        /// <inheritdoc/>
        [JsonPropertyName("summary")]
        string ISharedSharedMusic.Summary => Summary;

        /// <inheritdoc/>
        [JsonPropertyName("jumpUrl")]
        string ISharedSharedMusic.JumpUrl => JumpUrl;

        /// <inheritdoc/>
        [JsonPropertyName("pictureUrl")]
        string ISharedSharedMusic.PictureUrl => PictureUrl;

        /// <inheritdoc/>
        [JsonPropertyName("musicUrl")]
        string ISharedSharedMusic.MusicUrl => MusicUrl;

        /// <inheritdoc/>
        [JsonPropertyName("brief")]
        string ISharedSharedMusic.Brief => Brief;
#endif
    }
}
