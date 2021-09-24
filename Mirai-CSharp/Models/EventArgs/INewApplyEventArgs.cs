namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供通用申请相关信息的接口。继承自 <see cref="IApplyResponseArgs"/>
    /// </summary>
    public interface INewApplyEventArgs : IApplyResponseArgs
    {
        /// <summary>
        /// 申请人的昵称或群名片
        /// </summary>
        string NickName { get; }

        /// <summary>
        /// 申请消息
        /// </summary>
        string Message { get; }
    }

    /// <summary>
    /// 提供通用申请相关信息的接口。继承自 <see cref="INewApplyEventArgs"/> 和 <see cref="IApplyResponseArgs{TRawdata}"/>
    /// </summary>
    public interface INewApplyEventArgs<TRawdata> : INewApplyEventArgs, IApplyResponseArgs<TRawdata>
    {
        
    }
}
