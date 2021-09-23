using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供已执行的指令相关信息的接口
    /// </summary>
    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    [MappableMiraiHttpMessageKey("CommandExecutedEvent")]
    public interface ICommandExecutedEventArgs : IMiraiHttpMessage
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
        IChatMessage[] Args { get; }
        /// <summary>
        /// 指令发送者QQ号
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>当指令通过好友消息发送时为好友QQ号</item>
        /// <item>当指令通过群组消息发送时为发送者QQ号</item>
        /// <item>当指令通过其他方式发送时为 <see langword="null"/></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("friend")]
        long? Sender { get; }
        /// <summary>
        /// 指令发送者所在的群号
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>当指令通过好友消息发送时为 <see langword="null"/></item>
        /// <item>当指令通过群组消息发送时为发送者所在群号</item>
        /// <item>当指令通过其他方式发送时为 <see langword="null"/></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("member")]
        long? Group { get; }
    }

    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public class CommandExecutedEventArgs : MiraiHttpMessage, ICommandExecutedEventArgs
    {
        public string Name { get; set; } = null!;

        public IChatMessage[] Args { get; set; } = null!;

        public long? Sender { get; set; }

        public long? Group { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommandExecutedEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public CommandExecutedEventArgs(string name, IChatMessage[] args, long? sender, long? group)
        {
            Name = name;
            Args = args;
            Sender = sender;
            Group = group;
        }
    }
}
