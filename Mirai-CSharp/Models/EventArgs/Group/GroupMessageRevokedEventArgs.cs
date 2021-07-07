using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群内撤回消息的相关信息接口。继承自 <see cref="IGroupOperatingEventArgs"/> 和 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    public interface IGroupMessageRevokedEventArgs : IGroupOperatingEventArgs, IMessageRevokedEventArgs
    {

    }

    public class GroupMessageRevokedEventArgs : MessageRevokedEventArgs, IGroupMessageRevokedEventArgs
    {
        /// <summary>
        /// 被撤回消息所在群信息
        /// </summary>
        public IGroupInfo Group { get; set; } = null!;
        /// <summary>
        /// 进行撤回操作的用户信息
        /// </summary>
        public IGroupMemberInfo Operator { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageRevokedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public GroupMessageRevokedEventArgs(IGroupInfo group, IGroupMemberInfo @operator, long senderId, int messageId, DateTime sentTime) : base(senderId, messageId, sentTime)
        {
            Group = group;
            Operator = @operator;
        }
    }
}
