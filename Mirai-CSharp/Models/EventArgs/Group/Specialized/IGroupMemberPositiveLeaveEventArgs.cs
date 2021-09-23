namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供成员主动离群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberPositiveLeaveEventArgs : IMemberEventArgs
    {

    }

    /// <summary>
    /// 提供成员主动离群相关信息的接口。继承自 <see cref="IGroupMemberPositiveLeaveEventArgs"/> 和 <see cref="IMemberEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberPositiveLeaveEventArgs<TRawdata> : IGroupMemberPositiveLeaveEventArgs, IMemberEventArgs<TRawdata>
    {

    }
}
