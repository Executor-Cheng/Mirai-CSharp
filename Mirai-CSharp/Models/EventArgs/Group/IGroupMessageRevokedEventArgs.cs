namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群内撤回消息的相关信息接口。继承自 <see cref="IGroupOperatingEventArgs"/> 和 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    public interface IGroupMessageRevokedEventArgs : IGroupOperatingEventArgs, IMessageRevokedEventArgs
    {

    }

    /// <summary>
    /// 提供群内撤回消息的相关信息接口。继承自 <see cref="IGroupMessageRevokedEventArgs"/>, <see cref="IGroupOperatingEventArgs{TRawdata}"/> 和 <see cref="IMessageRevokedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMessageRevokedEventArgs<TRawdata> : IGroupMessageRevokedEventArgs, IGroupOperatingEventArgs<TRawdata>, IMessageRevokedEventArgs<TRawdata>
    {

    }
}
