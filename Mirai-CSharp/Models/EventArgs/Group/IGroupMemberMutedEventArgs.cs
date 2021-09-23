namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供其它群成员被禁言事件相关信息的接口。继承自 <see cref="IMemberOperatingEventArgs"/> 和 <see cref="IMutedEventArgs"/>
    /// </summary>
    public interface IGroupMemberMutedEventArgs : IMemberOperatingEventArgs, IMutedEventArgs
    {

    }

    /// <summary>
    /// 提供其它群成员被禁言事件相关信息的接口。继承自 <see cref="IGroupMemberMutedEventArgs"/> 和 <see cref="IMemberOperatingEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberMutedEventArgs<TRawdata> : IGroupMemberMutedEventArgs, IMemberOperatingEventArgs<TRawdata>
    {

    }
}
