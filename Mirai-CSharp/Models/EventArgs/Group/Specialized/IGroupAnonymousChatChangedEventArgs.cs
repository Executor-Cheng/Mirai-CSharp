namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供匿名聊天设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupAnonymousChatChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供匿名聊天设置被改变相关信息的接口。继承自 <see cref="IGroupAnonymousChatChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, Boolean}"/>
    /// </summary>
    public interface IGroupAnonymousChatChangedEventArgs<TRawdata> : IGroupAnonymousChatChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, bool>
    {

    }
}
