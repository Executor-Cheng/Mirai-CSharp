namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotKickedOutEventArgs : IGroupEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IBotKickedOutEventArgs"/> 和 <see cref="IGroupEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotKickedOutEventArgs<TRawdata> : IBotKickedOutEventArgs, IGroupEventArgs<TRawdata>
    {

    }
}
