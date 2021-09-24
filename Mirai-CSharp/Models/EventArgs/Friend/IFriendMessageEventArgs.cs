namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IFriendMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 好友信息
        /// </summary>
        IFriendInfo Sender { get; }
    }

    /// <summary>
    /// 提供好友消息的相关信息接口。继承自 <see cref="IFriendMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IFriendMessageEventArgs<TRawdata> : IFriendMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {
        
    }
}
