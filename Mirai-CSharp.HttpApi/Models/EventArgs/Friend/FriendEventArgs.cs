using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Utility.JsonConverters;
using ISharedFriendEventArgs = Mirai.CSharp.Models.EventArgs.IFriendEventArgs;
using ISharedFriendInfo = Mirai.CSharp.Models.IFriendInfo;
using ISharedJsonFriendEventArgs = Mirai.CSharp.Models.EventArgs.IFriendEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    public interface IFriendEventArgs : ISharedJsonFriendEventArgs, IMiraiHttpMessage
    {
        [JsonPropertyName("friend")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        new IFriendInfo Friend { get; }

#if !NETSTANDARD2_0
        [JsonPropertyName("friend")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedFriendInfo, FriendInfo>))]
        ISharedFriendInfo ISharedFriendEventArgs.Friend => Friend;
#endif
    }

    public abstract class FriendEventArgs : MiraiHttpMessage, IFriendEventArgs
    {
        [JsonPropertyName("friend")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<IFriendInfo, FriendInfo>))]
        public IFriendInfo Friend { get; set; } = null!;

        protected FriendEventArgs()
        {

        }

        protected FriendEventArgs(IFriendInfo friend)
        {
            Friend = friend;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("friend")]
        [JsonConverter(typeof(ChangeTypeJsonConverter<ISharedFriendInfo, FriendInfo>))]
        ISharedFriendInfo ISharedFriendEventArgs.Friend => Friend;
#endif
    }
}
