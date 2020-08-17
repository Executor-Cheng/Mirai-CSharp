namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 指示如何处理加群请求
    /// </summary>
    public enum GroupApplyActions
    {
        /// <summary>
        /// 同意入群
        /// </summary>
        Allow = 0,
        /// <summary>
        /// 拒绝入群
        /// </summary>
        Deny = 1,
        /// <summary>
        /// 忽略请求
        /// </summary>
        Ignore = 2,
        /// <summary>
        /// 拒绝入群并添加黑名单, 不再接收该用户的入群申请
        /// </summary>
        DenyAndBlock = 3,
        /// <summary>
        /// 忽略入群并添加黑名单, 不再接收该用户的入群申请
        /// </summary>
        IgnoreAndBlock = 4
    }
}
