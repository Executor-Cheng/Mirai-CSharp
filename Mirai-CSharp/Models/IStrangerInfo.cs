namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供陌生人信息的接口
    /// </summary>
    public interface IStrangerInfo : IBaseInfo
    {
        /// <summary>
        /// 陌生人备注(?)
        /// </summary>
        string Remark { get; }
    }
}
