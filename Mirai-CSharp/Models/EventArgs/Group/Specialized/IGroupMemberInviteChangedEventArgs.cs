namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群员邀请好友加群设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberInviteChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供群员邀请好友加群设置被改变相关信息的接口。继承自 <see cref="IGroupMemberInviteChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, Boolean}"/>
    /// </summary>
    public interface IGroupMemberInviteChangedEventArgs<TRawdata> : IGroupMemberInviteChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, bool>
    {

    }
}
