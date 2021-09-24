namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动重新登录信息的接口。继承自 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotReloginEventArgs : IBotEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动重新登录信息的接口。继承自 <see cref="IBotReloginEventArgs"/> 和 <see cref="IBotEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotReloginEventArgs<TRawdata> : IBotReloginEventArgs, IBotEventArgs<TRawdata>
    {

    }
}
