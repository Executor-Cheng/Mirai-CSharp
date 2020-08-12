using System.Collections.Generic;

namespace Mirai_CSharp.Models
{
    /// <summary>
    /// 提供临时消息的相关信息接口。继承自 <see cref="IGroupMessageBaseEventArgs"/>
    /// </summary>
    public interface ITempMessageEventArgs : IGroupMessageBaseEventArgs
    {
        
    }

    public class TempMessageEventArgs : GroupMessageBaseEventArgs, ITempMessageEventArgs
    {
        public TempMessageEventArgs()
        {

        }

        public TempMessageEventArgs(IMessageBase[] chain, IGroupMemberInfo sender) : base(chain, sender)
        {

        }

        public override string ToString()
            => $"[{Sender.Group.Name}({Sender.Group.Id})] {Sender.Name}(Temp {Sender.Id}) -> {string.Join("", (IEnumerable<Messages>)Chain)}";
    }
}
