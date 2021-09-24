namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友昵称变更相关信息接口。继承自 <see cref="IFriendEventArgs"/>
    /// </summary>
    public interface IFriendNickChangedEventArgs : IFriendEventArgs, IPropertyChangedEventArgs<string>
    {
        
    }

    /// <summary>
    /// 提供好友昵称变更相关信息接口。继承自 <see cref="IFriendNickChangedEventArgs"/>, <see cref="IFriendEventArgs{TRawdata}"/> 和 <see cref="IPropertyChangedEventArgs{TRawdata, TProperty}"/>
    /// </summary>
    public interface IFriendNickChangedEventArgs<TRawdata> : IFriendNickChangedEventArgs, IFriendEventArgs<TRawdata>, IPropertyChangedEventArgs<TRawdata, string>
    {

    }
}
