using System.Text.Json;
using System.Text.Json.Serialization;
using Mirai.CSharp.Framework.Models.General;
using Mirai.CSharp.Models.EventArgs;

namespace Mirai.CSharp.HttpApi.Models
{
    /// <summary>
    /// 表示由mirai-api-http收到的消息
    /// </summary>
    /// <remarks>
    /// 继承自 <see cref="IMiraiMessage{TRawdata}"/>
    /// </remarks>
    public interface IMiraiHttpMessage : IMiraiMessage<JsonElement>
    {
#if NETSTANDARD2_0
        [JsonIgnore]
        new JsonElement Rawdata { get; }

        [JsonIgnore]
        new bool BlockRemainingHandlers { get; set; }
#else
        [JsonIgnore]
        abstract JsonElement IMessage<JsonElement>.Rawdata { get; }

        [JsonIgnore]
        abstract bool IMessage.BlockRemainingHandlers { get; set; }
#endif
    }

    /// <summary>
    /// 实现 <see cref="MiraiHttpMessage"/> 的抽象类
    /// </summary>
    public abstract class MiraiHttpMessage : MiraiMessage<JsonElement>, IMiraiHttpMessage
    {
        [JsonIgnore]
        public sealed override JsonElement Rawdata { get => base.Rawdata; set => base.Rawdata = value; }

        [JsonIgnore]
        public override bool BlockRemainingHandlers { get => base.BlockRemainingHandlers; set => base.BlockRemainingHandlers = value; }

#if NETSTANDARD2_0
        [JsonIgnore]
        JsonElement IMessage<JsonElement>.Rawdata => Rawdata;
        
        [JsonIgnore]
        bool IMessage.BlockRemainingHandlers { get => BlockRemainingHandlers; set => BlockRemainingHandlers = value; }
#endif
    }
}
