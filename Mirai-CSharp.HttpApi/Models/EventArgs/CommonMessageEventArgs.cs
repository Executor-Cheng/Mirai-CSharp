using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedChatMessage = Mirai.CSharp.Models.ChatMessages.IChatMessage;
using ISharedCommonMessageEventArgs = Mirai.CSharp.Models.EventArgs.ICommonMessageEventArgs;
using ISharedJsonElementCommonMessageEventArgs = Mirai.CSharp.Models.EventArgs.ICommonMessageEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供通用消息的相关信息接口。继承自 <see cref="ISharedJsonElementCommonMessageEventArgs"/> 和 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public interface ICommonMessageEventArgs : ISharedJsonElementCommonMessageEventArgs, IMiraiHttpMessage
    {
        /// <inheritdoc cref="ISharedCommonMessageEventArgs.Chain"/>
        [JsonPropertyName("messageChain")]
        new IChatMessage[] Chain { get; }

#if !NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("messageChain")]
        ISharedChatMessage[] ISharedCommonMessageEventArgs.Chain => Chain;
#endif
    }

    [ResolveJsonConverter(typeof(ChatMessageJsonConverter))]
    public abstract class CommonMessageEventArgs : MiraiHttpMessage, ICommonMessageEventArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("messageChain")]
        public IChatMessage[] Chain { get; set; } = null!;

        protected CommonMessageEventArgs() { }

        protected CommonMessageEventArgs(IChatMessage[] chain)
        {
            Chain = chain;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("messageChain")]
        ISharedChatMessage[] ISharedCommonMessageEventArgs.Chain => Chain;
#endif
    }
}
