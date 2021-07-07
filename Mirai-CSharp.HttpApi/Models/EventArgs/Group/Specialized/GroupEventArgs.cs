namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供Bot加入了一个新群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotJoinedGroupEventArgs : IGroupEventArgs
    {

    }

    /// <summary>
    /// 提供Bot主动退出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotPositiveLeaveGroupEventArgs : IGroupEventArgs
    {

    }

    /// <summary>
    /// 提供Bot被踢出一个群相关信息的接口。继承自 <see cref="IGroupEventArgs"/>
    /// </summary>
    public interface IBotKickedOutEventArgs : IGroupEventArgs
    {

    }
}
