namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请相关信息的接口。继承自 <see cref="ICommonGroupApplyEventArgs"/>
    /// </summary>
    public interface IGroupApplyEventArgs : ICommonGroupApplyEventArgs
    {

    }

    /// <summary>
    /// 提供入群申请相关信息的接口。继承自 <see cref="IGroupApplyEventArgs"/> 和 <see cref="ICommonGroupApplyEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupApplyEventArgs<TRawdata> : IGroupApplyEventArgs, ICommonGroupApplyEventArgs<TRawdata>
    {

    }
}
