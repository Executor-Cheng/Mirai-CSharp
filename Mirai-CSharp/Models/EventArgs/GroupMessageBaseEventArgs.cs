using System.Collections.Generic;

namespace Mirai_CSharp.Models.EventArgs
{
    /// <summary>
    /// 提供群消息和临时消息的相关信息基接口。继承自 <see cref="ICommonMessageEventArgs"/>
    /// </summary>
    public interface IGroupMessageBaseEventArgs : ICommonMessageEventArgs
    {
        IGroupMemberInfo Sender { get; }
    }

    public abstract class GroupMessageBaseEventArgs : CommonMessageEventArgs, IGroupMessageBaseEventArgs
    {
        public IGroupMemberInfo Sender { get; set; } = null!;

        protected GroupMessageBaseEventArgs() { }

        protected GroupMessageBaseEventArgs(IMessageBase[] chain, IGroupMemberInfo sender) : base(chain)
        {
            Sender = sender;
        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<Messages>)Chain)}";
    }
}
