namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供好友申请相关信息的接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
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
