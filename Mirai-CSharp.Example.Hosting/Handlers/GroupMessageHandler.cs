using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai.CSharp.Builders;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example.Hosting.Handlers
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IGroupMessageEventArgs, GroupMessageEventArgs>))]
    public sealed class GroupMessageHandler : IMiraiHttpMessageHandler<IGroupMessageEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IGroupMessageEventArgs e) // 法3: 使用 IMessageBuilder
        {
            IMessageChainBuilder builder = session.GetMessageChainBuilder();
            builder.AddPlainMessage($"收到了来自{e.Sender.Name}[{e.Sender.Id}]{{{e.Sender.Permission}}}的群消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                 / 发送者群名片 /  / 发送者QQ号 /   /   发送者在群内权限   /                                                       / 消息链 /
            // builder.AddPlainMessage("QAQ").AddPlainMessage("OvO")/* .AddAtMessage(123456) etc... */;
            await session.SendGroupMessageAsync(e.Sender.Group.Id, builder/*, plain2, /* etc... */); // 向消息来源群异步发送由以上chain表示的消息
            // e.BlockRemainingHandlers = true; // 默认不阻断消息传递。如需阻断请删除注释
        }
    }
}
