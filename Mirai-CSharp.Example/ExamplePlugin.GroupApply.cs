using Mirai.CSharp.Models;
using System.Threading.Tasks;

namespace Mirai.CSharp.Example
{
    public partial class ExamplePlugin
    {
        public async Task<bool> GroupApply(MiraiHttpSession session, IGroupApplyEventArgs e)
        {
            await session.HandleGroupApplyAsync(e, GroupApplyActions.Deny, "略略略");
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 GroupApplyActions 枚举去处理请求
            // 你也可以暂存 IGroupApplyEventArgs e, 之后再调用session处理
            return false;
        }
    }
}
