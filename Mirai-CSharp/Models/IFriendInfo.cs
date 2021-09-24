namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供好友信息的接口。继承自 <see cref="IBaseInfo"/>
    /// </summary>
    public interface IFriendInfo : IBaseInfo
    {
        /// <summary>
        /// 好友备注
        /// </summary>
        string Remark { get; }
    }
}
