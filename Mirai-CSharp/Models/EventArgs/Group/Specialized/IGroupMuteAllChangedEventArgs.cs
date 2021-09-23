namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供全员禁言设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMuteAllChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供全员禁言设置被改变相关信息的接口。继承自 <see cref="IGroupMuteAllChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, Boolean}"/>
    /// </summary>
    public interface IGroupMuteAllChangedEventArgs<TRawdata> : IGroupMuteAllChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, bool>
    {

    }
}
