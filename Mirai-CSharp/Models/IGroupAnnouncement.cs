using System;

namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群公告基本信息的接口
    /// </summary>
    public interface IGroupAnnouncement
    {
        /// <summary>
        /// 群公告Id
        /// </summary>
        string Id { get; }
        /// <summary>
        /// 群信息
        /// </summary>
        IGroupInfo Group { get; }
        /// <summary>
        /// 群公告内容
        /// </summary>
        string Content { get; }
        /// <summary>
        /// 发布者QQ号
        /// </summary>
        long Sender { get; }
        /// <summary>
        /// 是否所有群成员已确认
        /// </summary>
        bool AllMemberConfirmed { get; }
        /// <summary>
        /// 已确认群成员人数
        /// </summary>
        int ConfirmedMembersCount { get; }
        /// <summary>
        /// 发布时间
        /// </summary>
        DateTime CreateTime { get; }
    }
}
