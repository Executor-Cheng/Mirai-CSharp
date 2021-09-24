using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Example
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IGroupApplyEventArgs, GroupApplyEventArgs>))]
    public partial class ExamplePlugin : IMiraiHttpMessageHandler<IGroupApplyEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IGroupApplyEventArgs e)
        {
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 GroupApplyActions 枚举去处理请求
            // 你也可以暂存 IGroupApplyEventArgs e, 之后再调用session处理
            await session.HandleGroupApplyAsync(e, GroupApplyActions.Deny, "略略略");
        }
    }
}
