namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberJoinedEventArgs : IMemberEventArgs, IInviterEventArgs
    {

    }

    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="IGroupMemberJoinedEventArgs"/> 和 <see cref="IMemberEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberJoinedEventArgs<TRawdata> : IGroupMemberJoinedEventArgs, IMemberEventArgs<TRawdata>, IInviterEventArgs<TRawdata>
    {

    }
}
