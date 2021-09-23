namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群信息的接口
    /// </summary>
    public interface IGroupConfig
    {
        /// <summary>
        /// 群名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 群公告
        /// </summary>
        string Announcement { get; }
        /// <summary>
        /// 是否允许坦白说
        /// </summary>
        bool? ConfessTalk { get; }
        /// <summary>
        /// 是否允许群成员邀请新用户
        /// </summary>
        bool? MemberInvite { get; }
        /// <summary>
        /// 是否自动通过入群申请
        /// </summary>
        bool? AutoApprove { get; }
        /// <summary>
        /// 是否允许匿名聊天
        /// </summary>
        bool? AnonymousChat { get; }
    }
}
