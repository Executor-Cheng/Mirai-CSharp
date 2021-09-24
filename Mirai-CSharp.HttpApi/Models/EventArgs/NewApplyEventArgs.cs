using System.Text.Json.Serialization;
using ISharedJsonElementNewApplyEventArgs = Mirai.CSharp.Models.EventArgs.INewApplyEventArgs<System.Text.Json.JsonElement>;
using ISharedNewApplyEventArgs = Mirai.CSharp.Models.EventArgs.INewApplyEventArgs;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供通用申请相关信息的接口。继承自 <see cref="ISharedJsonElementNewApplyEventArgs"/>, <see cref="IApplyResponseArgs"/> 和 <see cref="IMiraiHttpMessage"/>
    /// </summary>
    public interface INewApplyEventArgs : ISharedJsonElementNewApplyEventArgs, IApplyResponseArgs , IMiraiHttpMessage
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="ISharedNewApplyEventArgs.NickName"/>
        [JsonPropertyName("nick")]
        new string NickName { get; }

        /// <inheritdoc cref="ISharedNewApplyEventArgs.Message"/>
        [JsonPropertyName("message")]
        new string Message { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("nick")]
        abstract string ISharedNewApplyEventArgs.NickName { get; }

        /// <inheritdoc/>
        [JsonPropertyName("message")]
        abstract string ISharedNewApplyEventArgs.Message { get; }
#endif
    }

    public abstract class NewApplyEventArgs : ApplyResponseArgs, INewApplyEventArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("nick")]
        public string NickName { get; set; } = null!;

        /// <inheritdoc/>
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        protected NewApplyEventArgs() { }

        protected NewApplyEventArgs(long eventId, long fromGroup, long fromQQ, string nickName, string message) : base(eventId, fromQQ, fromGroup)
        {
            NickName = nickName;
            Message = message;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("nick")]
        string ISharedNewApplyEventArgs.NickName => NickName;

        /// <inheritdoc/>
        [JsonPropertyName("message")]
        string ISharedNewApplyEventArgs.Message => Message;
#endif
    }
}
