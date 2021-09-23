namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot意外断开连接信息的接口。继承自 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotDroppedEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot意外断开连接信息的接口。继承自 <see cref="IBotDroppedEventArgs"/>, <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotDroppedEventArgs<TRawdata> : IBotDroppedEventArgs, IBotEventArgs<TRawdata>
    {

    }
}
