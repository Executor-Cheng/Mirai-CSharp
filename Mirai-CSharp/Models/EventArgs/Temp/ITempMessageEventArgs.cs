namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供临时消息的相关信息接口。继承自 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    public interface ITempMessageEventArgs : IGroupMessageBaseEventArgs
    {

    }

    /// <summary>
    /// 提供临时消息的相关信息接口。继承自 <see cref="ITempMessageEventArgs"/> 和 <see cref="IGroupMessageBaseEventArgs{TRawdata}"/>
    /// </summary>
    public interface ITempMessageEventArgs<TRawdata> : ITempMessageEventArgs, IGroupMessageBaseEventArgs<TRawdata>
    {

    }
}
