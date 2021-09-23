using System.Diagnostics;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedAtAllMessage = Mirai.CSharp.Models.ChatMessages.IAtAllMessage;

namespace Mirai.CSharp.HttpApi.Models.ChatMessages
{
    /// <inheritdoc cref="ISharedAtAllMessage"/>
    [MappableMiraiChatMessageKey(AtAllMessage.MsgType)]
    public interface IAtAllMessage : ISharedAtAllMessage, IChatMessage
    {

    }

    [DebuggerDisplay("{ToString(),nq}")]
    public class AtAllMessage : ChatMessage, IAtAllMessage
    {
        public const string MsgType = "AtAll";

        [JsonPropertyName("type")]
        public override string Type => MsgType;
        /// <summary>
        /// 初始化 <see cref="AtAllMessage"/> 类的新实例
        /// </summary>
        public AtAllMessage()
        {

        }
        /// <inheritdoc/>
        public override string ToString()
            => "[mirai:atall]";
    }
}
