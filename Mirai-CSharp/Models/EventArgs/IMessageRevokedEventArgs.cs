using System;

namespace Mirai.CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供撤回消息的通用信息接口
    /// </summary>
    public interface IMessageRevokedEventArgs : IMiraiMessage
    {
        /// <summary>
        /// 原消息发送者的QQ号
        /// </summary>
        long SenderId { get; }
        /// <summary>
        /// 原消息Id
        /// </summary>
        int MessageId { get; }
        /// <summary>
        /// 原消息发送时间
        /// </summary>
        DateTime SentTime { get; }
    }

    /// <summary>
    /// 提供撤回消息的通用信息接口。继承自 <see cref="IMessageRevokedEventArgs"/>
    /// </summary>
    public interface IMessageRevokedEventArgs<TRawdata> : IMessageRevokedEventArgs, IMiraiMessage<TRawdata>
    {

    }
}
