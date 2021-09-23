using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IGroupMessageEventArgs, GroupMessageEventArgs>))]
    public class HttpApiPlugin : MiraiHttpMessageHandler<IGroupMessageEventArgs>, // .NET Framework 只能继承 MiraiHttpMessageHandler<TMessage> / DedicateMiraiHttpMessageHandler<TMessage>
                                 IMiraiHttpMessageHandler<IGroupMessageEventArgs> // .NET Core 起, 你应该直接实现 IMiraiHttpMessageHandler<TMessage> / IDedicateMiraiHttpMessageHandler<TMessage> 接口
    {
        // 使用 .NET Core 时, 删去 override 和 基类继承
        public override Task HandleMessageAsync(IMiraiHttpSession session, IGroupMessageEventArgs message)
        {
            return Task.CompletedTask;
        }
    }
}
