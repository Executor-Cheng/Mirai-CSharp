namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群头衔改动相关信息的接口。继承自 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberSpecialTitleChangedEventArgs : IGroupMemberPropertyChangedEventArgs<string>
    {

    }

    /// <summary>
    /// 提供群头衔改动相关信息的接口。继承自 <see cref="IGroupMemberSpecialTitleChangedEventArgs"/> 和 <see cref="IGroupMemberPropertyChangedEventArgs{TRawdata, String}"/>
    /// </summary>
    public interface IGroupMemberSpecialTitleChangedEventArgs<TRawdata> : IGroupMemberSpecialTitleChangedEventArgs, IGroupMemberPropertyChangedEventArgs<TRawdata, string>
    {

    }
}
