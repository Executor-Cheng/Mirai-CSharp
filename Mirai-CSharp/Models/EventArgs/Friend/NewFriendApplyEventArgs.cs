namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友申请相关信息的接口。继承自 <see cref="INewApplyEventArgs"/>
    /// </summary>
    public interface INewFriendApplyEventArgs : INewApplyEventArgs
    {

    }

    /// <summary>
    /// 提供好友申请相关信息的接口。继承自 <see cref="INewFriendApplyEventArgs"/> 和 <see cref="INewApplyEventArgs{TRawdata}"/>
    /// </summary>
    public interface INewFriendApplyEventArgs<TRawdata> : INewFriendApplyEventArgs, INewApplyEventArgs<TRawdata>
    {

    }
}
