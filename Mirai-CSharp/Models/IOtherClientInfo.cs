namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供其他客户端信息的接口
    /// </summary>
    public interface IOtherClientInfo : IBaseInfo
    {
        /// <summary>
        /// 其他客户端的平台
        /// </summary>
        string Platform { get; }
    }
}
