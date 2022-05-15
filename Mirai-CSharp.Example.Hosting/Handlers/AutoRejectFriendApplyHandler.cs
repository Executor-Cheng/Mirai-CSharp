using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Example.Hosting.Handlers
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<INewFriendApplyEventArgs, NewFriendApplyEventArgs>))]
    public partial class AutoRejectFriendApplyHandler : IMiraiHttpMessageHandler<INewFriendApplyEventArgs>
    {
        private readonly ILogger<AutoRejectFriendApplyHandler> _logger;

        public AutoRejectFriendApplyHandler(ILogger<AutoRejectFriendApplyHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleMessageAsync(IMiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            _logger.LogWarning($"已自动拒绝加好友请求。来源:{e.FromQQ}");
            return session.HandleNewFriendApplyAsync(e, FriendApplyAction.Deny, "略略略");
            // 把整个事件信息直接作为第一个参数即可, 然后根据自己需要选择一个 FriendApplyAction 枚举去处理请求
            // 你也可以暂存 INewFriendApplyEventArgs e, 之后再调用 session 处理
        }
    }
}
