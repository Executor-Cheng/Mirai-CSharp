using System;
using System.Collections.Generic;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供好友消息的相关信息接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IFriendMessageEventArgs : ICommonMessageEventArgs
    {
        IFriendInfo Sender { get; set; }
    }

    public class FriendMessageEventArgs : CommonMessageEventArgs, IFriendMessageEventArgs
    {
        public IFriendInfo Sender { get; set; } = null!;

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageEventArgs() { }

        [Obsolete("此类不应由用户主动创建实例。")]
        public FriendMessageEventArgs(IMessageBase[] chain, IFriendInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"{Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<Messages>)Chain)}";
    }
}
