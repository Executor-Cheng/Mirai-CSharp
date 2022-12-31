using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedJsonElementStrangerSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.IStrangerSyncMessageEventArgs<System.Text.Json.JsonElement>;
using ISharedStrangerInfo = Mirai.CSharp.Models.IStrangerInfo;
using ISharedStrangerSyncMessageEventArgs = Mirai.CSharp.Models.EventArgs.IStrangerSyncMessageEventArgs;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供陌生人同步消息的相关信息接口。继承自 <see cref="ISharedJsonElementStrangerSyncMessageEventArgs"/> 和 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("StrangerSyncMessage")]
    public interface IStrangerSyncMessageEventArgs : ISharedJsonElementStrangerSyncMessageEventArgs, ICommonMessageEventArgs
    {
        /// <inheritdoc cref="ISharedStrangerSyncMessageEventArgs.Subject"/>
        [JsonConverter(typeof(ChangeTypeJsonConverter<IStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("subject")]
        new IStrangerInfo Subject { get; }

#if !NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("subject")]
        ISharedStrangerInfo ISharedStrangerSyncMessageEventArgs.Subject => Subject;
#endif
    }

    public class StrangerSyncMessageEventArgs : CommonMessageEventArgs, IStrangerSyncMessageEventArgs
    {
        [JsonConverter(typeof(ChangeTypeJsonConverter<IStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("subject")]
        public IStrangerInfo Subject { get; set; }

        public StrangerSyncMessageEventArgs()
        {

        }

        public StrangerSyncMessageEventArgs(IChatMessage[] chain, IStrangerInfo subject) : base(chain)
        {
            Subject = subject;
        }

#if NETSTANDARD2_0
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedStrangerInfo, StrangerInfo>))]
        [JsonPropertyName("subject")]
        ISharedStrangerInfo ISharedStrangerSyncMessageEventArgs.Subject => Subject;
#endif
    }
}
