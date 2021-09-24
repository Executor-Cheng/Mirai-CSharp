using System.Threading.Tasks;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Models.EventArgs;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Example
{
    // 从 (I)MiraiMessageHandler<IMiraiSession, TMessage> 继承(实现), 且 TMessage 位于 Mirai.CSharp.Models.EventArgs 时, 将处理任何实现框架的消息, 包括但不限于 HttpApi, Native
    // 意味着你无需引用 Mirai-CSharp.HttpApi
    public class MiraiPlugin : MiraiMessageHandler<IMiraiSession, IGroupMessageEventArgs>, // .NET Framework 只能继承 MiraiMessageHandler<TClient, TMessage>
                               IMiraiMessageHandler<IMiraiSession, IGroupMessageEventArgs> // .NET Core 起, 你应该直接实现 IMiraiMessageHandler<TClient, TMessage> 接口
    {
        // 使用 .NET Core 时, 删去 override 和 基类继承
        public override Task HandleMessageAsync(IMiraiSession session, IGroupMessageEventArgs message)
        {
            return Task.CompletedTask;
        }
    }
}
