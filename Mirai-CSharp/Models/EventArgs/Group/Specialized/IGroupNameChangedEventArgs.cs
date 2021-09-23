namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群名称改变相关信息的接口。继承自 <see cref="IGroupPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupNameChangedEventArgs : IGroupPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供群名称改变相关信息的接口。继承自 <see cref="IGroupNameChangedEventArgs"/> 和 <see cref="IGroupPropertyChangedEventArgs{TRawdata, String}"/>
    /// </summary>
    public interface IGroupNameChangedEventArgs<TRawdata> : IGroupNameChangedEventArgs, IGroupPropertyChangedEventArgs<TRawdata, string>
    {

    }
}
