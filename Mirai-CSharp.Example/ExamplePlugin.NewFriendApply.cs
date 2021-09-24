using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Models;
using System.Threading.Tasks;

namespace Mirai.CSharp.Example
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<INewFriendApplyEventArgs, NewFriendApplyEventArgs>))]
    public partial class ExamplePlugin : IMiraiHttpMessageHandler<INewFriendApplyEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            await session.HandleNewFriendApplyAsync(e, FriendApplyAction.Deny, "略略略");
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 FriendApplyAction 枚举去处理请求
            // 你也可以暂存 INewFriendApplyEventArgs e, 之后再调用 session 处理
            e.BlockRemainingHandlers = false;
        }
    }
}
