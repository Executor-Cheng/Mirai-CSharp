namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 指示如何处理添加好友请求
    /// </summary>
    public enum FriendApplyAction
    {
        /// <summary>
        /// 同意添加好友
        /// </summary>
        Allow = 0,
        /// <summary>
        /// 拒绝添加好友
        /// </summary>
        Deny = 1,
        /// <summary>
        /// 拒绝添加好友并添加黑名单, 不再接收该用户的好友申请
        /// </summary>
        DenyAndBlock = 2
    }
}
