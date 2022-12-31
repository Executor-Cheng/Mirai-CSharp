namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供陌生人消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IStrangerMessageEventArgs : ICommonMessageEventArgs
    {
        /// <summary>
        /// 来源陌生人信息
        /// </summary>
        IStrangerInfo Sender { get; }
    }

    /// <summary>
    /// 提供陌生人消息的相关信息接口。继承自 <see cref="IStrangerMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs{TRawdata}"/>
    /// </summary>
    public interface IStrangerMessageEventArgs<TRawdata> : IStrangerMessageEventArgs, ICommonMessageEventArgs<TRawdata>
    {

    }
}
