using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Builders;
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
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IGroupMessageEventArgs, GroupMessageEventArgs>))]
    public partial class ExamplePlugin : IMiraiHttpMessageHandler<IGroupMessageEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IGroupMessageEventArgs e) // 法1: 使用 IMessageBase[]
        {
            // 临时消息和群消息一致, 不多写例子了
            IChatMessage[] chain = new IChatMessage[]
            {
                new PlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}")
                //                          / 发送者群名片 /  / 发送者QQ号 /   /   发送者在群内权限   /                                                       / 消息链 /
                // 你还可以在这里边加入更多的 IMessageBase
            };
            await session.SendGroupMessageAsync(e.Sender.Group.Id, chain); // 向消息来源群异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }

        public async Task HandleMessageAsync2(IMiraiHttpSession session, IGroupMessageEventArgs e) // 法2: 使用 params IMessageBase[]
        {
            IChatMessage plain1 = new PlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                                / 发送者群名片 /  / 发送者QQ号 /   /   发送者在群内权限   /                                                       / 消息链 /
            IChatMessage plain2 = new PlainMessage("QAQ"); // 在下边的 SendGroupMessageAsync, 你可以串起n个 IMessageBase
            await session.SendGroupMessageAsync(e.Sender.Group.Id, plain1/*, plain2, /* etc... */); // 向消息来源群异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }

        public async Task HandleMessageAsync3(IMiraiHttpSession session, IGroupMessageEventArgs e) // 法3: 使用 IMessageBuilder
        {
            IMessageChainBuilder builder = new MessageChainBuilder();
            builder.AddPlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                 / 发送者群名片 /  / 发送者QQ号 /   /   发送者在群内权限   /                                                       / 消息链 /
            // builder.AddPlainMessage("QAQ").AddPlainMessage("QwQ")/* .AddAtMessage(123456) etc... */;
            // 你甚至可以一开始 new MessageBuilder() 的时候就开始 Chaining
            await session.SendGroupMessageAsync(e.Sender.Group.Id, builder/*, plain2, /* etc... */); // 向消息来源群异步发送由以上chain表示的消息
            e.BlockRemainingHandlers = false; // 不阻断消息传递。如需阻断请返回true
        }
    }
}
