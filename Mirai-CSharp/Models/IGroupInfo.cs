namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群内权限和基本信息的接口。继承自 <see cref="IBaseInfo"/>
    /// </summary>
    public interface IGroupInfo : IBaseInfo
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        GroupPermission Permission { get; }
    }
}
