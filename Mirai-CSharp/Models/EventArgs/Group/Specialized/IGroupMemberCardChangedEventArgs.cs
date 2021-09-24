namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群名片改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberCardChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供群名片改动相关信息的接口。继承自 <see cref="IGroupMemberCardChangedEventArgs"/> 和 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata, String}"/>
    /// </summary>
    public interface IGroupMemberCardChangedEventArgs<TRawdata> : IGroupMemberCardChangedEventArgs, IGroupMemberPropertyChangedEventArgs<TRawdata, string>
    {

    }
}
