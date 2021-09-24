namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供Bot主动退出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotPositiveLeaveGroupEventArgs : IGroupEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动退出一个群相关信息的接口。继承自 <see cref="IBotPositiveLeaveGroupEventArgs"/> 和 <see cref="IGroupEventArgs{TRawdata}"/>
    /// </summary>
    public interface IBotPositiveLeaveGroupEventArgs<TRawdata> : IBotPositiveLeaveGroupEventArgs, IGroupEventArgs<TRawdata>
    {

    }
}
