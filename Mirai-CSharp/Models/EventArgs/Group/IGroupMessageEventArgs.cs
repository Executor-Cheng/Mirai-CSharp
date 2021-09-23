namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群消息的相关信息接口。继承自 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    public interface IGroupMessageEventArgs : IGroupMessageBaseEventArgs
    {

    }

    /// <summary>
    /// 提供群消息的相关信息接口。继承自 <see cref="IGroupMessageEventArgs"/> 和 <see cref="IGroupMessageBaseEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMessageEventArgs<TRawdata> : IGroupMessageEventArgs, IGroupMessageBaseEventArgs<TRawdata>
    {

    }
}
