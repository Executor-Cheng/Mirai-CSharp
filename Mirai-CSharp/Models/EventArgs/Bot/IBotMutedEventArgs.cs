namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被禁言事件相关信息的接口。继承自 <see cref="IOperatorEventArgs"/> 和 <see cref="IMutedEventArgs"/>
    /// </summary>
    public interface IBotMutedEventArgs : IOperatorEventArgs, IMutedEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被禁言事件相关信息的接口。继承自 <see cref="IBotMutedEventArgs"/> 和 <see cref="IOperatorEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotMutedEventArgs<TRawdata> : IBotMutedEventArgs, IOperatorEventArgs<TRawdata>
    {

    }
}
