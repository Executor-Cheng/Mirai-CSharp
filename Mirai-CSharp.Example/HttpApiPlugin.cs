using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    // 为此消息处理类标定所需要使用到的消息解析器
    // 标定的特性仅在使用 IMessageFrameworkBuilder.AddHandler 和 IMessageFrameworkBuilder.ResolveParser 时才会被解析
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IFriendMessageEventArgs, FriendMessageEventArgs>))]
    public partial class HttpApiPlugin : MiraiHttpMessageHandler<IFriendMessageEventArgs>, // .NET Framework 只能继承 MiraiHttpMessageHandler<TMessage> / DedicateMiraiHttpMessageHandler<TMessage>
                                         IMiraiHttpMessageHandler<IFriendMessageEventArgs> // .NET Core 起, 你应该直接实现 IMiraiHttpMessageHandler<TMessage> / IDedicateMiraiHttpMessageHandler<TMessage> 接口
    {
        private readonly ILogger<HttpApiPlugin> _logger;

        public HttpApiPlugin(ILogger<HttpApiPlugin> logger)
        {
            _logger = logger;
        }

        // 使用 .NET Core 时, 删去 override 和 基类继承
        public override Task HandleMessageAsync(IMiraiHttpSession session, IFriendMessageEventArgs message)
        {
            LogFriendMessage(_logger, message.Sender.Name, message.Sender.Id, string.Join(null, (IEnumerable<IChatMessage>)message.Chain));
            //                        /    来源QQ昵称     / /    来源QQ号     / /                      消息链的字符串表示                      /
            return Task.CompletedTask;
        }

        [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "{name}[{fromQQ}]:{message}")]
        protected static partial void LogFriendMessage(ILogger logger, string name, long fromQQ, string message);
    }
}
