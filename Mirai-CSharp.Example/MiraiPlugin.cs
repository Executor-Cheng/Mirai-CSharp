using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Models.ChatMessages;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Example
{
    // 从 (I)MiraiMessageHandler<IMiraiSession, TMessage> 继承(实现), 且 TMessage 位于 Mirai.CSharp.Models.EventArgs 时, 将处理任何实现框架的消息, 包括但不限于 HttpApi, Native
    // 意味着你无需引用 Mirai-CSharp.HttpApi
    public partial class MiraiPlugin : MiraiMessageHandler<IMiraiSession, IGroupMessageEventArgs>, // .NET Framework 只能继承 MiraiMessageHandler<TClient, TMessage>
                                       IMiraiMessageHandler<IMiraiSession, IGroupMessageEventArgs> // .NET Core 起, 你应该直接实现 IMiraiMessageHandler<TClient, TMessage> 接口
    {
        private readonly ILogger<MiraiPlugin> _logger;

        public MiraiPlugin(ILogger<MiraiPlugin> logger)
        {
            _logger = logger;
        }

        // 使用 .NET Core 时, 删去 override 和 基类继承
        public override Task HandleMessageAsync(IMiraiSession session, IGroupMessageEventArgs message)
        {
            LogGroupMessage(_logger, message.Sender.Group.Id, message.Sender.Name, message.Sender.Id, string.Join(null, (IEnumerable<IChatMessage>)message.Chain));
            //                       /        来源群号       / /    来源QQ昵称      / /    来源QQ号     / /                      消息链的字符串表示                      /
            return Task.CompletedTask;
        }

        [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "[{groupNumber}] {name}[{fromQQ}]:{message}")]
        protected static partial void LogGroupMessage(ILogger logger, long groupNumber, string name, long fromQQ, string message);
    }
}
