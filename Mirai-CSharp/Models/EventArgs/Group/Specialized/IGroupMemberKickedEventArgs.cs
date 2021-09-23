namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供成员被踢出群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberKickedEventArgs : IMemberOperatingEventArgs
    {

    }

    /// <summary>
    /// 提供成员被踢出群相关信息的接口。继承自 <see cref="IGroupMemberKickedEventArgs"/> 和 <see cref="IMemberEventArgs{TRawdata}"/>
    /// </summary>
    public interface IGroupMemberKickedEventArgs<TRawdata> : IGroupMemberKickedEventArgs, IMemberOperatingEventArgs<TRawdata>
    {

    }
}
