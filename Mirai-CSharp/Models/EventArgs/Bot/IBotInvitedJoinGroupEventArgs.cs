namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被邀请入群相关信息的接口。继承自 <see cref="ICommonGroupApplyEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotInvitedJoinGroupEventArgs : ICommonGroupApplyEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被邀请入群相关信息的接口。继承自 <see cref="ICommonGroupApplyEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotInvitedJoinGroupEventArgs<TRawdata> : IBotInvitedJoinGroupEventArgs, ICommonGroupApplyEventArgs<TRawdata>
    {

    }
}
