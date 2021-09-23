namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被挤下线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotKickedOfflineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被挤下线信息的接口。继承自 <see cref="IBotKickedOfflineEventArgs"/> 和 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotKickedOfflineEventArgs<TRawdata> : IBotKickedOfflineEventArgs, IBotEventArgs<TRawdata>
    {

    }
}
