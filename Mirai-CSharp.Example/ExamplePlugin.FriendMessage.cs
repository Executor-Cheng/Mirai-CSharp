using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;

#pragma warning disable CA1822 // Mark members as static // 示例方法禁用Information
#pragma warning disable IDE0059 // 不需要赋值 // 禁用+1
namespace Mirai.CSharp.Example
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IFriendMessageEventArgs, FriendMessageEventArgs>))]
    public partial class ExamplePlugin : IMiraiHttpMessageHandler<IFriendMessageEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IFriendMessageEventArgs e) // 法1: 使用 IMessageBase[]
        {
            IChatMessage[] chain = new IChatMessage[]
            {
                new PlainMessage($"收到了来自{e.Sender.Name}({e.Sender.Remark})[{e.Sender.Id}]的私聊消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}")
                //                          /   好友昵称  /  /    好友备注    /  /  好友QQ号  /                                                        / 消息链 /
                // 你还可以在这里边加入更多的 IMessageBase
            };
            await session.SendFriendMessageAsync(e.Sender.Id, chain); // 向消息来源好友异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }

        public async Task HandleMessageAsync2(IMiraiHttpSession session, IFriendMessageEventArgs e) // 法2: 使用 params IMessageBase[]
        {
            IChatMessage plain1 = new PlainMessage($"收到了来自{e.Sender.Name}({e.Sender.Remark})[{e.Sender.Id}]的私聊消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                                /   好友昵称  /  /    好友备注    /  /  好友QQ号  /                                                        / 消息链 /
            IChatMessage plain2 = new PlainMessage("嘤嘤嘤"); // 在下边的 SendFriendMessageAsync, 你可以串起n个 IChatMessage
            await session.SendFriendMessageAsync(e.Sender.Id, plain1/*, plain2, /* etc... */); // 向消息来源好友异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }

        public async Task HandleMessageAsync3(IMiraiHttpSession session, IFriendMessageEventArgs e) // 法3: 使用 params IMessageBase[]
        {
            Builders.IMessageChainBuilder builder = session.GetMessageChainBuilder();
            builder.AddPlainMessage($"收到了来自{e.Sender.Name}({e.Sender.Remark})[{e.Sender.Id}]的私聊消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                 /   好友昵称  /  /    好友备注    /  /  好友QQ号  /                                                        / 消息链 /
            // builder.AddPlainMessage("QAQ").AddPlainMessage("QwQ")/* .AddAtMessage(123456) etc... */;
            // 你甚至可以一开始 new MessageBuilder() 的时候就开始 Chaining
            await session.SendFriendMessageAsync(e.Sender.Id, builder); // 向消息来源群异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }
    }
}
