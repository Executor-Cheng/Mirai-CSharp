using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Parsers;
using Mirai.CSharp.HttpApi.Parsers.Attributes;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    // 所有实现了 I(Dedicate)MiraiHttpMessageHandler<TMessage> 接口的实现类实例都可以通过 IMiraiHttpSession.Add(Remove)Plugin 实时添加/移除
    // 但请注意, 其标定的 RegisterMiraiHttpParserAttribute 无法被实时解析
    // 请用户在配置 IServiceCollection 时提前调用 IMiraiHttpFrameworkBuilder.ResolveParser<THandler>
    // 以令框架提前解析其所需要使用到的消息解析器, 否则消息处理方法将不会被调用
    [RegisterMiraiHttpParser(typeof(DefaultMappableMiraiHttpMessageParser<IGroupMessageEventArgs, GroupMessageEventArgs>))]
    public class DynamicPlugin : MiraiHttpMessageHandler<IGroupMessageEventArgs>,
                                 IMiraiHttpMessageHandler<IGroupMessageEventArgs>
    {
        // 使用 .NET Core 时, 删去 override 和 基类继承
        public override Task HandleMessageAsync(IMiraiHttpSession session, IGroupMessageEventArgs message)
        {
            return Task.CompletedTask;
        }
    }
}
