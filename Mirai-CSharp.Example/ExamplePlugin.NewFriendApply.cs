using Mirai.CSharp.Models;
using System.Threading.Tasks;

namespace Mirai.CSharp.Example
{
    public partial class ExamplePlugin
    {
        public async Task<bool> NewFriendApply(MiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            await session.HandleNewFriendApplyAsync(e, FriendApplyAction.Deny, "略略略");
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 FriendApplyAction 枚举去处理请求
            // 你也可以暂存 INewFriendApplyEventArgs e, 之后再调用 session 处理
            return false;
        }
    }
}
