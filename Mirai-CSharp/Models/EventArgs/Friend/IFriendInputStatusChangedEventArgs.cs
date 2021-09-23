namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友输入状态变更相关信息接口。继承自 <see cref="IFriendEventArgs"/>
    /// </summary>
    public interface IFriendInputStatusChangedEventArgs : IFriendEventArgs
    {
        bool Inputting { get; }
    }

    /// <summary>
    /// 提供好友输入状态变更相关信息接口。继承自 <see cref="IFriendInputStatusChangedEventArgs"/> 和 <see cref="IFriendEventArgs{TRawdata}"/>
    /// </summary>
    public interface IFriendInputStatusChangedEventArgs<TRawdata> : IFriendInputStatusChangedEventArgs, IFriendEventArgs<TRawdata>
    {

    }
}
