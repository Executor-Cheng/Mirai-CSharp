namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被解除禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/>
    /// </summary>
    public interface IBotUnmutedEventArgs : IOperatorEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被解除禁言事件相关信息的接口。继承自 <see cref="IBotUnmutedEventArgs"/> 和 <see cref="IOperatorEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotUnmutedEventArgs<TRawdata> : IBotUnmutedEventArgs, IOperatorEventArgs<TRawdata>
    {

    }
}
