using System;
using System.Text.Json.Serialization;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using ISharedFriendNickChangedEventArgs = Mirai.CSharp.Models.EventArgs.IFriendNickChangedEventArgs<System.Text.Json.JsonElement>;

namespace Mirai.CSharp.HttpApi.Models.EventArgs
{
    /// <summary>
    /// 提供好友昵称变更相关信息接口。继承自 <see cref="ISharedFriendNickChangedEventArgs"/> 和 <see cref="IPropertyChangedEventArgs{TProperty}"/>
    /// </summary>
    [MappableMiraiHttpMessageKey("FriendNickChangedEvent")]
    public interface IFriendNickChangedEventArgs : ISharedFriendNickChangedEventArgs, IPropertyChangedEventArgs<string>
    {
#if NETSTANDARD2_0
        /// <inheritdoc cref="IPropertyChangedEventArgs{TProperty}.Origin"/>
        [JsonPropertyName("from")]
        new string Origin { get; }

        /// <inheritdoc cref="IPropertyChangedEventArgs{TProperty}.Current"/>
        [JsonPropertyName("to")]
        new string Current { get; }
#else
        /// <inheritdoc/>
        [JsonPropertyName("from")]
        abstract string Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<string>.Origin { get; }
        /// <inheritdoc/>
        [JsonPropertyName("to")]
        abstract string Mirai.CSharp.Models.EventArgs.IPropertyChangedEventArgs<string>.Current { get; }
#endif
    }

    public class FriendNickChangedEventArgs : FriendEventArgs, IFriendNickChangedEventArgs
    {
        [JsonPropertyName("from")]
        public string Origin { get; set; } = null!;

        [JsonPropertyName("to")]
        public string Current { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendNickChangedEventArgs()
        {

        }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendNickChangedEventArgs(IFriendInfo friend, string origin, string current) : base(friend)
        {
            Origin = origin;
            Current = current;
        }

#if NETSTANDARD2_0
        /// <inheritdoc/>
        [JsonPropertyName("from")]
        string IPropertyChangedEventArgs<string>.Origin => Origin;

        /// <inheritdoc/>
        [JsonPropertyName("to")]
        string IPropertyChangedEventArgs<string>.Current => Current;
#endif
    }
}
