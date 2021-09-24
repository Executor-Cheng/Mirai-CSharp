using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedJsonElementOtherClientMessageEventArgs = Mirai.CSharp.Models.EventArgs.IOtherClientMessageEventArgs<System.Text.Json.JsonElement>;
using ISharedOtherClientInfo = Mirai.CSharp.Models.IOtherClientInfo;
using ISharedOtherClientMessageEventArgs = Mirai.CSharp.Models.EventArgs.IOtherClientMessageEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs.OtherClient
{
    /// <summary>
    /// 提供其它客户端消息的相关信息接口。继承自 <see cref="ISharedJsonElementOtherClientMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("OtherClientMessage")]
    public interface IOtherClientMessageEventArgs : ISharedJsonElementOtherClientMessageEventArgs, ICommonMessageEventArgs
    {
        [JsonPropertyName("sender")]
        new IOtherClientInfo Sender { get; }

#if !NETSTANDARD2_0
        [JsonPropertyName("sender")]
        ISharedOtherClientInfo ISharedOtherClientMessageEventArgs.Sender => Sender;
#endif
    }

    public class OtherClientMessageEventArgs : CommonMessageEventArgs, IOtherClientMessageEventArgs
    {
        [JsonPropertyName("sender")]
        public IOtherClientInfo Sender { get; set; } = null!;

        public OtherClientMessageEventArgs()
        {
        }

        public OtherClientMessageEventArgs(IChatMessage[] chain, IOtherClientInfo sender) : base(chain)
        {
            Sender = sender;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("sender")]
        ISharedOtherClientInfo ISharedOtherClientMessageEventArgs.Sender => Sender;
#endif
    }
}
