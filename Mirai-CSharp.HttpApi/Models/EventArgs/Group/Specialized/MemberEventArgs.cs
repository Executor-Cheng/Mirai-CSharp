namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供新人入群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberJoinedEventArgs : IMemberEventArgs
    {

    }

    /// <summary>
    /// 提供成员主动离群相关信息的接口。继承自 <see cref="IMemberEventArgs"/>
    /// </summary>
    public interface IGroupMemberPositiveLeaveEventArgs : IMemberEventArgs
    {

    }
}
