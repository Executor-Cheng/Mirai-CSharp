using Mirai_CSharp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mirai_CSharp.Example
{
    public partial class ExamplePlugin
    {
        public async Task<bool> FriendMessage(MiraiHttpSession session, IFriendMessageEventArgs e)
        {
            IMessageBase[] chain = new IMessageBase[]
            {
                new PlainMessage($"收到了来自{e.Sender.Name}({e.Sender.Remark})[{e.Sender.Id}]的私聊消息:{string.Join(null, (IEnumerable<IMessageBase>)e.Chain)}")
                //                          /   好友昵称  /  /    好友备注    /  /  好友QQ号  /                                                        / 消息链 /
            };
            await session.SendFriendMessageAsync(e.Sender.Id, chain); // 向消息来源好友异步发送由以上chain表示的消息
            return false; // 不阻断消息传递。如需阻断请返回true
        }
    }
}
