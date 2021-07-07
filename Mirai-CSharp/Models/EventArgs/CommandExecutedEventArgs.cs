using System;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供已执行的指令相关信息的接口
    /// </summary>
    public interface ICommandExecutedEventArgs : IEventArgsBase
    {
        /// <summary>
        /// 指令名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 指令参数
        /// </summary>
        string[] Args { get; }
        /// <summary>
        /// 指令发送者QQ号
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>当指令通过好友消息发送时为好友QQ号</item>
        /// <item>当指令通过群组消息发送时为发送者QQ号</item>
        /// <item>当指令通过其他方式发送时为0</item>
        /// </list>
        /// </remarks>
        long Sender { get; }
        /// <summary>
        /// 指令发送者所在的群号
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>当指令通过好友消息发送时为0</item>
        /// <item>当指令通过群组消息发送时为发送者所在群号</item>
        /// <item>当指令通过其他方式发送时为0</item>
        /// </list>
        /// </remarks>
        long Group { get; }
    }

    /// <summary>
    /// 已执行的指令相关信息的消息类
    /// </summary>
    public class CommandExecutedEventArgs : EventArgsBase, ICommandExecutedEventArgs
    {
        public string Name { get; set; } = null!;

        public string[] Args { get; set; } = null!;

        public long Sender { get; set; }

        public long Group { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommandExecutedEventArgs() { }
    }
}
