namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot登录成功信息的接口。继承自 <see cref="IBotEventArgs"/>
    /// </summary>
    public interface IBotOnlineEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot登录成功信息的接口。继承自 <see cref="IBotOnlineEventArgs"/> 和 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotOnlineEventArgs<TRawdata> : IBotOnlineEventArgs, IBotEventArgs<TRawdata>
    {

    }
}
