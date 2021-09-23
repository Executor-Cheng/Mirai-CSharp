namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动离线信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotPositiveOfflineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动离线信息的接口。继承自 <see cref="IBotPositiveOfflineEventArgs"/> 和 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotPositiveOfflineEventArgs<TRawdata> : IBotPositiveOfflineEventArgs, IBotEventArgs<TRawdata>
    {

    }
}
