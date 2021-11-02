namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IGroupOperatingEventArgs"/>
    /// </summary>
    public interface IBotKickedOutEventArgs : IGroupOperatingEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IBotKickedOutEventArgs"/> 和 <see cref="IGroupOperatingEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotKickedOutEventArgs<TRawdata> : IBotKickedOutEventArgs, IGroupOperatingEventArgs<TRawdata>
    {

    }
}
