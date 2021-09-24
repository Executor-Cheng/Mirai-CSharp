using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Builders;
using Mirai.CSharp.HttpApi.Builder;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    public static class Program // 前提: nuget Mirai-CSharp, 版本需要 >= 2.0.0
    {
        public static async Task Main()
        {
            // 一套能用的框架, 必须要注册至少一个 Invoker, Parser, Client 和 Handler
            // Invoker 负责消息调度
            // Parser 负责解析消息到具体接口以便调度器调用相关 Handler 下的处理方法
            // Client 负责收发原始数据
            IServiceProvider services = new ServiceCollection().AddMiraiBaseFramework()   // 表示使用基于基础框架的构建器
                                                               .AddHandler<MiraiPlugin>() // 虽然可以把 HttpApiPlugin 作为泛型参数塞入, 但不建议这么做
                                                               .Services
                                                               .AddDefaultMiraiHttpFramework() // 表示使用 mirai-api-http 实现的构建器
                                                               .ResolveParser<DynamicPlugin>() // 只提前解析 DynamicPlugin 将要用到的消息解析器
                                                               .AddInvoker<MiraiHttpMessageHandlerInvoker>() // 使用默认的调度器
                                                               .AddHandler<HttpApiPlugin>() // 可以如此添加更多消息处理器
                                                               .AddClient<MiraiHttpSession>() // 使用默认的客户端
                                                               .Services
                                                               // 由于 MiraiHttpSession 使用 IOptions<MiraiHttpSessionOptions>, 其作为 Singleton 被注册
                                                               // 配置此项将配置基于此 IServiceProvider 全局的连接配置
                                                               // 如果你想一个作用域一个配置的话
                                                               // 自行做一个实现类, 继承MiraiHttpSession, 构造参数中使用 IOptionsSnapshot<MiraiHttpSessionOptions>
                                                               // 并将其传递给父类的构造参数
                                                               // 然后在每一个作用域中!先!配置好 IOptionsSnapshot<MiraiHttpSessionOptions>, 再尝试获取 IMiraiHttpSession
                                                               .Configure<MiraiHttpSessionOptions>(options =>
                                                               {
                                                                   options.Host = "域名/IP";
                                                                   options.Port = 12345; // 端口
                                                                   options.AuthKey = "00000000000000000000000000000000"; // 凭据
                                                               })
                                                               .AddLogging()
                                                               .BuildServiceProvider();
            IServiceScope scope = services.CreateScope();
            await using var x = (IAsyncDisposable)scope;
            //await using AsyncServiceScope scope = services.CreateAsyncScope(); // 自 .NET 6.0 起才可以如此操作代替上边两句
            services = scope.ServiceProvider;
            IMiraiHttpSession session = services.GetRequiredService<IMiraiHttpSession>(); // 大部分服务都基于接口注册, 请使用接口作为类型解析
            DynamicPlugin plugin = new DynamicPlugin();
            session.AddPlugin(plugin); // 实时添加
            await session.ConnectAsync(0); // 填入期望连接到的机器人QQ号
            while (true)
            {
                if (Console.ReadLine() == "exit")
                {
                    session.RemovePlugin(plugin); // 实时移除
                    break;
                }
            }
        }
    }
}
