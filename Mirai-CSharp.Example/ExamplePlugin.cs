using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mirai_CSharp.Example
{
    public class ExamplePlugin : IFriendMessage, // 你想处理什么事件就实现什么事件对应的接口
                                 IGroupMessage, 
                                 INewFriendApply,
                                 IGroupApply
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

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            // 临时消息和群消息一致, 不多写例子了
            IMessageBase[] chain = new IMessageBase[]
            {
                new PlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IMessageBase>)e.Chain)}")
                //                          / 发送者群名片 /  / 发送者QQ号 /   /   发送者在群内权限   /                                                       / 消息链 /
            };
            await session.SendGroupMessageAsync(e.Sender.Group.Id, chain); // 向消息来源群异步发送由以上chain表示的消息
            return false; // 不阻断消息传递。如需阻断请返回true
        }

        public async Task<bool> NewFriendApply(MiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            await session.HandleNewFriendApplyAsync(e, FriendApplyAction.Deny, "略略略"); 
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 FriendApplyAction 枚举去处理请求
            // 你也可以暂存 INewFriendApplyEventArgs e, 之后再调用 session 处理
            return false;
        }

        public async Task<bool> GroupApply(MiraiHttpSession session, IGroupApplyEventArgs e)
        {
            await session.HandleGroupApplyAsync(e, GroupApplyActions.Deny, "略略略");
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 GroupApplyActions 枚举去处理请求
            // 你也可以暂存 IGroupApplyEventArgs e, 之后再调用session处理
            return false;
        }

#pragma warning disable IDE0051 // 删除未使用的私有成员
        private async Task SendPictureAsync(MiraiHttpSession session, string path) // 发图
        {
            // 你也可以使用另一个重载 UploadPictureAsync(PictureTarget, Stream)
            // mirai-api-http 在v1.7.0以下时将使用本地的HttpListener做图片中转
            ImageMessage msg = await session.UploadPictureAsync(PictureTarget.Group, path);
            await session.SendGroupMessageAsync(0, new IMessageBase[] { msg }); // 自己填群号
        }
    }
}
