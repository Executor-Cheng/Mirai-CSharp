namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友撤回消息的相关信息接口。继承自 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    public interface IFriendMessageRevokedEventArgs : IMessageRevokedEventArgs
    {
        /// <summary>
        /// 进行撤回操作的QQ号
        /// </summary>
        long Operator { get; }
    }

    /// <summary>
    /// 提供好友撤回消息的相关信息接口。继承自 <see cref="IFriendMessageRevokedEventArgs"/> 和 <see cref="IMessageRevokedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IFriendMessageRevokedEventArgs<TRawdata> : IFriendMessageRevokedEventArgs, IMessageRevokedEventArgs<TRawdata>
    {
        
    }
}
