using System.Text.Json.Serialization;
using ISharedApplyResponseArgs = Mirai.CSharp.Models.EventArgs.IApplyResponseArgs;
using ISharedJsonElementApplyResponseArgs = Mirai.CSharp.Models.EventArgs.IApplyResponseArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供用于处理相关申请所需信息的接口。继承自 <see cref="ISharedJsonElementApplyResponseArgs"/>
    /// </summary>
    public interface IApplyResponseArgs : ISharedJsonElementApplyResponseArgs, IMiraiHttpMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedApplyResponseArgs.EventId"/>
        [JsonPropertyName("eventId")]
        new long EventId { get; }
        
        /// <inheritdoc cref="ISharedApplyResponseArgs.FromQQ"/>
        [JsonPropertyName("fromId")]
        new long FromQQ { get; }
        
        /// <inheritdoc cref="ISharedApplyResponseArgs.FromGroup"/>
        [JsonPropertyName("groupId")]
        new long FromGroup { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("eventId")]
        abstract long ISharedApplyResponseArgs.EventId { get; }

        /// <inheritdoc/>
        [JsonPropertyName("fromId")]
        abstract long ISharedApplyResponseArgs.FromQQ { get; }

        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        abstract long ISharedApplyResponseArgs.FromGroup { get; }
#endif
    }

    public abstract class ApplyResponseArgs : MiraiHttpMessage, IApplyResponseArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("fromId")]
        public long FromQQ { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        public long FromGroup { get; set; }

        protected ApplyResponseArgs()
        {

        }

        protected ApplyResponseArgs(long eventId, long fromQQ, long fromGroup)
        {
            EventId = eventId;
            FromQQ = fromQQ;
            FromGroup = fromGroup;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("eventId")]
        long ISharedApplyResponseArgs.EventId => EventId;

        /// <inheritdoc/>
        [JsonPropertyName("fromId")]
        long ISharedApplyResponseArgs.FromQQ { get; }

        /// <inheritdoc/>
        [JsonPropertyName("groupId")]
        long ISharedApplyResponseArgs.FromGroup { get; }
#endif
    }
}
