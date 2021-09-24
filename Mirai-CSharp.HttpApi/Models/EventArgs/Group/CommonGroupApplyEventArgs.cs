using System;
using System.Text.Json.Serialization;
using ISharedCommonGroupApplyEventArgs = Mirai.CSharp.Models.EventArgs.ICommonGroupApplyEventArgs;
using ISharedJsonCommonGroupApplyEventArgs = Mirai.CSharp.Models.EventArgs.ICommonGroupApplyEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供入群申请/受邀入群相关信息的基接口。继承自 <see cref="ISharedJsonCommonGroupApplyEventArgs"/> 和 <see cref="INewApplyEventArgs"/>
    /// </summary>
    public interface ICommonGroupApplyEventArgs : ISharedJsonCommonGroupApplyEventArgs, INewApplyEventArgs
    {
#if NETSTANDARD2_0
        /// <see cref="ISharedCommonGroupApplyEventArgs.FromGroupName"/>
        [JsonPropertyName("groupName")]
        new string FromGroupName { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("groupName")]
        abstract string ISharedCommonGroupApplyEventArgs.FromGroupName { get; }
#endif
    }

    public abstract class CommonGroupApplyEventArgs : NewApplyEventArgs, ICommonGroupApplyEventArgs
    {
        /// <inheritdoc/>
        [JsonPropertyName("groupName")]
        public string FromGroupName { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        protected CommonGroupApplyEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        protected CommonGroupApplyEventArgs(string fromGroupName, long eventId, long fromGroup, long fromQQ, string nickName, string message) : base(eventId, fromGroup, fromQQ, nickName, message)
        {
            FromGroupName = fromGroupName;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("groupName")]
        string ISharedCommonGroupApplyEventArgs.FromGroupName => FromGroupName;
#endif
    }
}
