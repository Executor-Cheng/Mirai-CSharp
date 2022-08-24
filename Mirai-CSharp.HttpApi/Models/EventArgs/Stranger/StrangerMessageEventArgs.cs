using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedJsonElementStrangerMessageEventArgs = Mirai.CSharp.Models.EventArgs.IStrangerMessageEventArgs<System.Text.Json.JsonElement>;
using ISharedStrangerInfo = Mirai.CSharp.Models.IStrangerInfo;
using ISharedStrangerMessageEventArgs = Mirai.CSharp.Models.EventArgs.IStrangerMessageEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs.Stranger
{
    /// <summary>
    /// 提供临时消息的相关信息接口。继承自 <see cref="ISharedJsonElementStrangerMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("StrangerMessage")]
    public interface IStrangerMessageEventArgs : ISharedJsonElementStrangerMessageEventArgs, ICommonMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<IStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("sender")]
        new IStrangerInfo Sender { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("sender")]
        ISharedStrangerInfo ISharedStrangerMessageEventArgs.Sender => Sender;
#endif
    }

    public class StrangerMessageEventArgs : CommonMessageEventArgs, IStrangerMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<IStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("sender")]
        public IStrangerInfo Sender { get; set; } = null!;

        public StrangerMessageEventArgs()
        {

        }

        public StrangerMessageEventArgs(IChatMessage[] chain, IStrangerInfo sender) : base(chain)
        {
            Sender = sender;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("sender")]
        ISharedStrangerInfo ISharedStrangerMessageEventArgs.Sender => Sender;
#endif
    }
}
