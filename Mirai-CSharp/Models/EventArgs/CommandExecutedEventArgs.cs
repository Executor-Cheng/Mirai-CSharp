using System;
using System.Text.Json.Serialization;

#pragma warning disable CA1819 // Properties should not return arrays
namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供已执行的指令相关信息的接口
    /// </summary>
    public interface ICommandExecutedEventArgs
    {
        /// <summary>
        /// 指令名称
        /// </summary>
        [JsonPropertyName("name")]
        string Name { get; }
        /// <summary>
        /// 指令参数
        /// </summary>
        [JsonPropertyName("args")]
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
        [JsonPropertyName("sender")]
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
        [JsonPropertyName("group")]
        long Group { get; }
    }

    public class CommandExecutedEventArgs : ICommandExecutedEventArgs
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("args")]
        public string[] Args { get; set; } = null!;

        [JsonPropertyName("sender")]
        public long Sender { get; set; }

        [JsonPropertyName("group")]
        public long Group { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommandExecutedEventArgs() { }
    }
}
