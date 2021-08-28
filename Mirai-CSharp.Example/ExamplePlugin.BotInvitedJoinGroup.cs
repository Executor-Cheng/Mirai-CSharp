using Mirai_CSharp_Test_efw561we5fwef65.Models;
using System.Threading.Tasks;

namespace Mirai_CSharp_Test_efw561we5fwef65.Example
{
    public partial class ExamplePlugin
    {
        public async Task<bool> BotInvitedJoinGroup(MiraiHttpSession session, IBotInvitedJoinGroupEventArgs e)
        {
            await session.HandleBotInvitedJoinGroupAsync(e, GroupApplyActions.Allow, "略略略"); // 在这个事件下, 只有 GroupApplyActions.Allow 和
                                                                                               //                 GroupApplyActions.Deny 有效
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 GroupApplyActions 枚举去处理请求
            // 你也可以暂存 IGroupApplyEventArgs e, 之后再调用session处理
            return false;
        }
    }
}
