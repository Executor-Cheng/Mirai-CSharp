namespace Mirai.CSharp.Models
{
    /// <summary>
    /// 提供群成员信息的接口。继承自 <see cref="IGroupInfo"/>
    /// </summary>
    public interface IGroupMemberInfo : IGroupInfo
    {
#if NETSTANDARD2_0
        /// <summary>
        /// 成员昵称
        /// </summary>
        new string Name { get; }
#else
        /// <summary>
        /// 成员昵称
        /// </summary>
        abstract string IBaseInfo.Name { get; }
#endif

        /// <summary>
        /// 机器人所在群的信息
        /// </summary>
        IGroupInfo Group { get; }
    }
}
