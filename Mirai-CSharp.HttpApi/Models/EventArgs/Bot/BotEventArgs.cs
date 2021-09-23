using System;
using System.Text.Json.Serialization;
using ISharedBotEventArgs = Mirai.CSharp.Models.EventArgs.IBotEventArgs;
using ISharedJsonElementBotEventArgs = Mirai.CSharp.Models.EventArgs.IBotEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <inheritdoc cref="ISharedJsonElementBotEventArgs"/>
    public interface IBotEventArgs : ISharedJsonElementBotEventArgs, IMiraiHttpMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedBotEventArgs.QQNumber"/>
        [JsonPropertyName("qq")]
        new long QQNumber { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("qq")]
        abstract long ISharedBotEventArgs.QQNumber { get; }
#endif
    }

    public abstract class BotEventArgs : MiraiHttpMessage, IBotEventArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("qq")]
        public long QQNumber { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected BotEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected BotEventArgs(long qqNumber)
        {
            QQNumber = qqNumber;
        }

#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedBotEventArgs.QQNumber"/>
        [JsonPropertyName("qq")]
        long ISharedBotEventArgs.QQNumber => QQNumber;
#endif
    }
}
