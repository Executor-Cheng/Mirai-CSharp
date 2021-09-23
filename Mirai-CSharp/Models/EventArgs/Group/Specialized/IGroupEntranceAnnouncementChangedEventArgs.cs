namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供入群公告改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupEntranceAnnouncementChangedEventArgs : IGroupPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供入群公告改变相关信息的接口。继承自 <see cref="IGroupEntranceAnnouncementChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, String}"/>
    /// </summary>
    public interface IGroupEntranceAnnouncementChangedEventArgs<TRawdata> : IGroupEntranceAnnouncementChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, string>
    {

    }
}
