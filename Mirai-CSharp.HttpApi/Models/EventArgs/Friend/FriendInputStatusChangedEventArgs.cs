using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedFriendInputStatusChangedEventArgs = Mirai.CSharp.Models.EventArgs.IFriendInputStatusChangedEventArgs;
using ISharedJsonFriendInputStatusChangedEventArgs = Mirai.CSharp.Models.EventArgs.IFriendInputStatusChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友输入状态变更相关信息接口。继承自 <see cref="ISharedJsonFriendInputStatusChangedEventArgs"/> 和 <see cref="IFriendEventArgs"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("FriendInputStatusChangedEvent")]
    public interface IFriendInputStatusChangedEventArgs : ISharedJsonFriendInputStatusChangedEventArgs, IFriendEventArgs
    {
#if !NETSTANDARD2_0
        [JsonPropertyName("inputting")]
        abstract bool ISharedFriendInputStatusChangedEventArgs.Inputting { get; }
#endif
    }

    public class FriendInputStatusChangedEventArgs : FriendEventArgs, IFriendInputStatusChangedEventArgs
    {
        [JsonPropertyName("inputting")]
        public bool Inputting { get; set; }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendInputStatusChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendInputStatusChangedEventArgs(IFriendInfo friend, bool inputting) : base(friend)
        {
            Inputting = inputting;
        }

#if NETSTANDARD2_0
        [JsonPropertyName("inputting")]
        bool ISharedFriendInputStatusChangedEventArgs.Inputting => Inputting;
#endif
    }
}
