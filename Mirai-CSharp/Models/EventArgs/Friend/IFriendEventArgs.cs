namespace Mirai.CSharp.Models.EventArgs
{
    public interface IFriendEventArgs : IMiraiMessage
    {
        IFriendInfo Friend { get; }
    }

    public interface IFriendEventArgs<TRawdata> : IFriendEventArgs, IMiraiMessage<TRawdata>
    {

    }
}
