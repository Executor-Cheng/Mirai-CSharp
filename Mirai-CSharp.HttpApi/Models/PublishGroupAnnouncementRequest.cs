using System;
using System.Text.Json.Serialization;
using ISharedPublishGroupAnnouncementRequest = Mirai.CSharp.Models.IPublishGroupAnnouncementRequest;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models
{
    public interface IPublishGroupAnnouncementRequest : ISharedPublishGroupAnnouncementRequest
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.GroupNumber"/>
        [JsonPropertyName("target")]
        new long GroupNumber { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.Content"/>
        [JsonPropertyName("content")]
        new string Content { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.SendToNewMember"/>
        [JsonPropertyName("sendToNewMember")]
        new bool? SendToNewMember { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.Pinned"/>
        [JsonPropertyName("pinned")]
        new bool? Pinned { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.ShowEditMemberCard"/>
        [JsonPropertyName("showEditCard")]
        new bool? ShowEditMemberCard { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.AutoPopup"/>
        [JsonPropertyName("showPopup")]
        new bool? AutoPopup { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.RequireConfirmation"/>
        [JsonPropertyName("requireConfirmation")]
        new bool? RequireConfirmation { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.ImageUrl"/>
        [JsonPropertyName("imageUrl")]
        new string? ImageUrl { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.ImagePath"/>
        [JsonPropertyName("imagePath")]
        new string? ImagePath { get; }
        /// <inheritdoc cref="ISharedPublishGroupAnnouncementRequest.ImageBase64"/>
        [JsonPropertyName("imageBase64")]
        new string? ImageBase64 { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("target")]
        abstract long ISharedPublishGroupAnnouncementRequest.GroupNumber { get; }
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        abstract string ISharedPublishGroupAnnouncementRequest.Content { get; }
        /// <inheritdoc/>
        [JsonPropertyName("sendToNewMember")]
        abstract bool? ISharedPublishGroupAnnouncementRequest.SendToNewMember { get; }
        /// <inheritdoc/>
        [JsonPropertyName("pinned")]
        abstract bool? ISharedPublishGroupAnnouncementRequest.Pinned { get; }
        /// <inheritdoc/>
        [JsonPropertyName("showEditCard")]
        abstract bool? ISharedPublishGroupAnnouncementRequest.ShowEditMemberCard { get; }
        /// <inheritdoc/>
        [JsonPropertyName("showPopup")]
        abstract bool? ISharedPublishGroupAnnouncementRequest.AutoPopup { get; }
        /// <inheritdoc/>
        [JsonPropertyName("requireConfirmation")]
        abstract bool? ISharedPublishGroupAnnouncementRequest.RequireConfirmation { get; }
        /// <inheritdoc/>
        [JsonPropertyName("imageUrl")]
        abstract string? ISharedPublishGroupAnnouncementRequest.ImageUrl { get; }
        /// <inheritdoc/>
        [JsonPropertyName("imagePath")]
        abstract string? ISharedPublishGroupAnnouncementRequest.ImagePath { get; }
        /// <inheritdoc/>
        [JsonPropertyName("imageBase64")]
        abstract string? ISharedPublishGroupAnnouncementRequest.ImageBase64 { get; }
#endif
        /// <summary>
        /// 会话密钥
        /// </summary>
        [JsonPropertyName("sessionKey")]
        string SessionKey { get; }
    }

    public class PublishGroupAnnouncementRequest : IPublishGroupAnnouncementRequest
    {
        /// <inheritdoc/>
        [JsonPropertyName("sessionKey")]
        public string SessionKey { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("target")]
        public long GroupNumber { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("sendToNewMember")]
        public bool? SendToNewMember { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("pinned")]
        public bool? Pinned { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("showEditCard")]
        public bool? ShowEditMemberCard { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("showPopup")]
        public bool? AutoPopup { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("requireConfirmation")]
        public bool? RequireConfirmation { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("imagePath")]
        public string? ImagePath { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("imageBase64")]
        public string? ImageBase64 { get; set; }

        /// <inheritdoc cref="PublishGroupAnnouncementRequest(long, string)"/>
        [Obsolete("请使用带参数的构造方法初始化本类实例。")]
        public PublishGroupAnnouncementRequest() { }

        /// <inheritdoc cref="PublishGroupAnnouncementRequest(long, string, bool?, bool?, bool?, bool?, bool?, string?, string?, string?)"/>
        public PublishGroupAnnouncementRequest(long groupNumber, string content) : this(groupNumber, content, null, null, null, null, null, null, null, null)
        {

        }

        /// <summary>
        /// 初始化 <see cref="PublishGroupAnnouncementRequest"/> 类的新实例
        /// </summary>
        /// <param name="groupNumber">要发布群公告的群号</param>
        /// <param name="content">群公告内容</param>
        /// <param name="sendToNewMember">是否发送给新成员</param>
        /// <param name="pinned">是否置顶</param>
        /// <param name="showEditMemberCard">是否显示群成员修改群名片的引导</param>
        /// <param name="autoPopup">是否自动弹出</param>
        /// <param name="requireConfirmation">是否需要群成员确认</param>
        /// <param name="imageUrl">群公告图片地址</param>
        /// <param name="imagePath">群公告图片路径</param>
        /// <param name="imageBase64">群公告图片base64编码</param>
        public PublishGroupAnnouncementRequest(long groupNumber, string content, bool? sendToNewMember, bool? pinned, bool? showEditMemberCard, bool? autoPopup, bool? requireConfirmation, string? imageUrl, string? imagePath, string? imageBase64)
        {
            GroupNumber = groupNumber;
            Content = content;
            SendToNewMember = sendToNewMember;
            Pinned = pinned;
            ShowEditMemberCard = showEditMemberCard;
            AutoPopup = autoPopup;
            RequireConfirmation = requireConfirmation;
            ImageUrl = imageUrl;
            ImagePath = imagePath;
            ImageBase64 = imageBase64;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("target")]
        long ISharedPublishGroupAnnouncementRequest.GroupNumber => GroupNumber;
        /// <inheritdoc/>
        [JsonPropertyName("content")]
        string ISharedPublishGroupAnnouncementRequest.Content => Content;
        /// <inheritdoc/>
        [JsonPropertyName("sendToNewMember")]
        bool? ISharedPublishGroupAnnouncementRequest.SendToNewMember => SendToNewMember;
        /// <inheritdoc/>
        [JsonPropertyName("pinned")]
        bool? ISharedPublishGroupAnnouncementRequest.Pinned => Pinned;
        /// <inheritdoc/>
        [JsonPropertyName("showEditCard")]
        bool? ISharedPublishGroupAnnouncementRequest.ShowEditMemberCard => ShowEditMemberCard;
        /// <inheritdoc/>
        [JsonPropertyName("showPopup")]
        bool? ISharedPublishGroupAnnouncementRequest.AutoPopup => AutoPopup;
        /// <inheritdoc/>
        [JsonPropertyName("requireConfirmation")]
        bool? ISharedPublishGroupAnnouncementRequest.RequireConfirmation => RequireConfirmation;
        /// <inheritdoc/>
        [JsonPropertyName("imageUrl")]
        string? ISharedPublishGroupAnnouncementRequest.ImageUrl => ImageUrl;
        /// <inheritdoc/>
        [JsonPropertyName("imagePath")]
        string? ISharedPublishGroupAnnouncementRequest.ImagePath => ImagePath;
        /// <inheritdoc/>
        [JsonPropertyName("imageBase64")]
        string? ISharedPublishGroupAnnouncementRequest.ImageBase64 => ImageBase64;
#endif
    }
}
