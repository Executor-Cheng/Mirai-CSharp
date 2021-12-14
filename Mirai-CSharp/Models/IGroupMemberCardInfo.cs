namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群名片和专属头衔的接口
    /// </summary>
    public interface IGroupMemberCardInfo
    {
        /// <summary>
        /// 群名片
        /// </summary>
        string? Name { get; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        string? SpecialTitle { get; }
    }
}
