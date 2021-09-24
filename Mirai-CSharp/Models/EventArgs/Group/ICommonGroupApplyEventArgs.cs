namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请/受邀入群相关信息的基接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
    public interface ICommonGroupApplyEventArgs : INewApplyEventArgs
    {
        /// <summary>
        /// 来源群名称
        /// </summary>
        string FromGroupName { get; }
    }

    /// <summary>
    /// 提供入群申请/受邀入群相关信息的基接口。继承自 <see cref="ICommonGroupApplyEventArgs"/> 和 <see cref="INewApplyEventArgs{TRawdata}"/>
    /// </summary>
    public interface ICommonGroupApplyEventArgs<TRawdata> : ICommonGroupApplyEventArgs, INewApplyEventArgs<TRawdata>
    {
        
    }
}
