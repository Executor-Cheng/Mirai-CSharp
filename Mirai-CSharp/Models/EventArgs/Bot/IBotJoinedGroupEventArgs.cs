namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotJoinedGroupEventArgs : IGroupEventArgs, IInviterEventArgs
    {

    }

    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="IBotJoinedGroupEventArgs"/> 和 <see cref="IGroupEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotJoinedGroupEventArgs<TRawdata> : IBotJoinedGroupEventArgs, IGroupEventArgs<TRawdata>, IInviterEventArgs<TRawdata>
    {

    }
}
