using System.Collections.Generic;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供群消息的相关信息接口。继承自 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    public interface IGroupMessageEventArgs : IGroupMessageBaseEventArgs
    {

    }

    public class GroupMessageEventArgs : GroupMessageBaseEventArgs, IGroupMessageEventArgs
    {
        public GroupMessageEventArgs()
        {

        }

        public GroupMessageEventArgs(IMessageBase[] chain, IGroupMemberInfo sender) : base(chain, sender)
        {

        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}({Sender.Id}) -> {string.Join("", (IEnumerable<MessageBase>)Chain)}";
    }
}
