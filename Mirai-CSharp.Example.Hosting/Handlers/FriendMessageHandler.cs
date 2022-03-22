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
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IFriendMessageEventArgs, FriendMessageEventArgs>))]
    public partial class FriendMessageHandler : IMiraiHttpMessageHandler<IFriendMessageEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IFriendMessageEventArgs e) // 法3: 使用 params IMessageBase[]
        {
            IMessageChainBuilder builder = session.GetMessageChainBuilder();
            builder.AddPlainMessage($"收到了来自{e.Sender.Name}({e.Sender.Remark})[{e.Sender.Id}]的私聊消息:{string.Join(null, (IEnumerable<IChatMessage>)e.Chain)}");
            //                                 /   好友昵称  /  /    好友备注    /  /  好友QQ号  /                                                        / 消息链 /
            // builder.AddPlainMessage("QAQ").AddPlainMessage("TvT")/* .AddAtMessage(123456) etc... */;
            // 你甚至可以一开始 new MessageBuilder() 的时候就开始 Chaining
            await session.SendFriendMessageAsync(e.Sender.Id, builder); // 向消息来源群异步发送由以上chain表示的消息
            // e.BlockRemainingHandlers = true; // 默认不阻断消息传递。如需阻断请删除注释
        }
    }
}
