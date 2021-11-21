using System;

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

        /// <summary>
        /// 成员特殊头衔
        /// </summary>
        /// <remarks>
        /// 如果连接到的 mirai-api-http 版本小于2.0, 本值恒为 <see langword="null"/>
        /// </remarks>
        public string? SpecialTitle { get; set; }

        /// <summary>
        /// 成员入群时间
        /// </summary>
        /// <remarks>
        /// 如果连接到的 mirai-api-http 版本小于2.0, 本值恒为 <see langword="null"/>
        /// </remarks>
        public DateTime? JoinTime { get; set; }

        /// <summary>
        /// 成员上次发言时间
        /// </summary>
        /// <remarks>
        /// 如果连接到的 mirai-api-http 版本小于2.0, 本值恒为 <see langword="null"/>
        /// </remarks>
        public DateTime? LastSpeakTime { get; set; }

        /// <summary>
        /// 成员禁言剩余时间
        /// </summary>
        /// <remarks>
        /// 如果连接到的 mirai-api-http 版本小于2.0, 本值恒为 <see langword="null"/>
        /// <br/>
        /// 如果成员未被禁言, 本值为 <see cref="TimeSpan.Zero"/>
        /// </remarks>
        public TimeSpan? MuteTimeRemaining { get; set; }
    }
}
