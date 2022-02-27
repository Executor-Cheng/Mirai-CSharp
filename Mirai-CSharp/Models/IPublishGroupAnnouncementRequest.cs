namespace Mirai.CSharp.Models
{
    public interface IPublishGroupAnnouncementRequest
    {
        /// <summary>
        /// 要发布群公告的群号
        /// </summary>
        long GroupNumber { get; }
        /// <summary>
        /// 群公告内容
        /// </summary>
        string Content { get; }
        /// <summary>
        /// 是否发送给新成员
        /// </summary>
        bool? SendToNewMember { get; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        bool? Pinned { get; }
        /// <summary>
        /// 是否显示群成员修改群名片的引导
        /// </summary>
        bool? ShowEditMemberCard { get; }
        /// <summary>
        /// 是否自动弹出
        /// </summary>
        bool? AutoPopup { get; }
        /// <summary>
        /// 是否需要群成员确认
        /// </summary>
        bool? RequireConfirmation { get; }
        /// <summary>
        /// 群公告图片地址
        /// </summary>
        string? ImageUrl { get; }
        /// <summary>
        /// 群公告图片路径
        /// </summary>
        string? ImagePath { get; }
        /// <summary>
        /// 群公告图片base64编码
        /// </summary>
        string? ImageBase64 { get; }
    }
}
