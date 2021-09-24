namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供坦白说设置被改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata, TProperty}"/>
    /// </summary>
    public interface IGroupConfessTalkChangedEventArgs : IGroupPropertyChangedEventArgs<bool>
    {

    }

    /// <summary>
    /// 提供坦白说设置被改变相关信息的接口。继承自 <see cref="IGroupConfessTalkChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, Boolean}"/>
    /// </summary>
    public interface IGroupConfessTalkChangedEventArgs<TRawdata> : IGroupConfessTalkChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, bool>
    {

    }
}
