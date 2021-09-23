namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供基本信息(Id和名称)的接口
    /// </summary>
    public interface IBaseInfo
    {
        /// <summary>
        /// QQ号/群号
        /// </summary>
        long Id { get; }
        /// <summary>
        /// 昵称/群名
        /// </summary>
        string Name { get; }
    }
}
