using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Handlers;
using Mirai.CSharp.Invoking;
using Mirai.CSharp.Parsers;
using Mirai.CSharp.Session;

namespace Mirai.CSharp.Builders
{
    public class MiraiBaseFrameworkBuilder : MiraiFrameworkBuilder<IMiraiMessageHandlerInvoker<IMiraiSession>, IMiraiSession, IMiraiMessageHandler, IMiraiMessageParser>
    {
        public MiraiBaseFrameworkBuilder(IServiceCollection services) : base(services)
        {

        }
    }
}
