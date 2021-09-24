namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供其它群成员被解除禁言事件相关信息的接口。继承自 <see cref="IMemberOperatingEventArgs"/>
    /// </summary>
    public interface IGroupMemberUnmutedEventArgs : IMemberOperatingEventArgs
    {

    }

    /// <summary>
    /// 提供其它群成员被解除禁言事件相关信息的接口。继承自 <see cref="IGroupMemberUnmutedEventArgs"/> 和 <see cref="IMemberOperatingEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberUnmutedEventArgs<TRawdata> : IGroupMemberUnmutedEventArgs, IMemberOperatingEventArgs<TRawdata>
    {

    }
}
