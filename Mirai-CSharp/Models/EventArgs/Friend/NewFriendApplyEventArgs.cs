namespace Mirai_CSharp.Models
{
    public interface INewFriendApplyEventArgs : INewApplyEventArgs
    {

    }

    public class NewFriendApplyEventArgs : NewApplyEventArgs, INewFriendApplyEventArgs
    {
        public NewFriendApplyEventArgs()
        {

        }

        public NewFriendApplyEventArgs(long eventId, long fromGroup, long fromQQ, string nickName) : base(eventId, fromGroup, fromQQ, nickName)
        {

        }
    }
}
