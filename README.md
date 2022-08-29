<div align="center">
    <h1>Mirai-CSharp</h1>
</div>
<div align="center">
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
        <img src="https://img.shields.io/nuget/v/Mirai-CSharp"></a>
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
    	<img src="https://img.shields.io/nuget/vpre/Mirai-CSharp"></a>
    <a href="https://www.nuget.org/packages/Mirai-CSharp">
	    <img src="https://img.shields.io/nuget/dt/Mirai-CSharp"></a>
    <img src="https://img.shields.io/github/last-commit/Executor-Cheng/Mirai-CSharp">
    <img src="https://img.shields.io/github/stars/Executor-Cheng/Mirai-CSharp">
</div>



## 关于本项目  
这是一个帮助C#开发者与 [Mirai](https://github.com/mamoe/mirai) 交互的项目  
它通过调用 [mirai-api-http](https://github.com/mamoe/mirai-api-http) 提供的 http-api 与其交互  

## 开始使用

### 安装
最简单的方式是从 nuget 上获取 Mirai-CSharp 的相关包, 并且我们也推荐你在 nuget 包管理器中为项目安装它, 不过你也可以手动克隆项目, 编译, 并直接引用链接库.

> 在使用 nuget 安装包时, 如若要使用最新功能, 请勾选 "包括预发行版"
>
> 从2.X版本开始, 本项目将使用依赖注入框架, 必须先注册相关服务才能使用。可选择这些常用的依赖注入框架: `Microsoft.Extensions.DependencyInjection`, `AutoFac`

### 内部实现解释
从2.X版本起, 作者已将包分离为 `Mirai-CSharp` 以及 `Mirai-CSharp.HttpApi`, 其中第一个中只包含框架的基本抽象, 第二个中包含的是其特定实现, 并且在2.X版本中, 与1.X版本发布的内容差异较大, 项目结构有巨大改变

在2.X版本中, 本框架将使用作者自己实现的消息框架进行消息分发。框架有以下五个组件：构建器(Builder)、调度器(Invoker)、处理器(Handler)、可选：解析器(Parser)、客户端(Client)。各组件功能如下:
1. 构建器(Builder): 负责在依赖注入框架中注册相关组件
2. 调度器(Invoker): 负责将消息实例传给合适的处理器(Handler), 也可使用合适的解析器(Parser)先处理原始数据为相应消息实例
3. 处理器(Handler): 负责处理相关客户端和消息实例的实现
4. 解析器(Parser): 负责解析原始数据为相应的消息实例
5. 客户端(Client): 负责收发原始数据，并调用调度器(Invoker)进行消息处理

以 `Mirai-CSharp.HttpApi` 为例, 从原始数据到消息处理器的简化版流程如下:

```
              mirai-api-http 
                    |
                 (json)
                    v
Mirai.CSharp.HttpApi.Session.IMiraiHttpSession (客户端)
                    |
             (JsonElement)
                    v
Mirai.CSharp.HttpApi.Invoking.IMiraiHttpMessageHandlerInvoker (调度器)
                    |
             (JsonElement)
                    v
Mirai.CSharp.HttpApi.Parsers.IMiraiHttpMessageParser (解析器)
                    |
     (Mirai.CSharp.HttpApi.Models.IMiraiHttpMessage)
                    v
 Mirai.CSharp.HttpApi.Invoking.IMiraiHttpMessageHandlerInvoker (调度器)
                    |
     (Mirai.CSharp.HttpApi.Models.IMiraiHttpMessage)
                    v
Mirai.CSharp.HttpApi.Handlers.IMiraiHttpMessageHandlerBase (处理器)
```

### 示例
> 以下示例将使用这三个包: `Microsoft.Extensions.DependencyInjection`, `Mirai-CSharp`， `Mirai-CSharp.HttpApi`, 请用户先行安装

下面以一个最简单的控制台程序为示例, 对 QQ 内的任何 at 自己了的群聊消息响应 "Hello world" 文本消息

在目前已正式发布的最新版本2.X中, `Mirai.CSharp` 的基础核心组件位于 `Mirai.CSharp`, `Mirai.CSharp.Models` 以及 `Mirai.CSharp.Builders` 命名空间中.

首先我们定义一个基于所有特定实现的用于打印 QQ 内的任何群聊消息类
```csharp
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
```

也可定义一个基于 `Mirai-CSharp.HttpApi`(`mirai-api-http`) 实现的用于打印 QQ 内的任何私聊消息类, 并为其标注所需要使用的消息解析器
```csharp
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

```

然后我们需要注册 `Mirai-CSharp` 和 `Mirai-CSharp.HttpApi` 的基础服务, 并注册上述消息处理器。
定义注册必要服务的方法和实际使用的方法, 具体请看注释: 

```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.Builders;
using Mirai.CSharp.HttpApi.Builder;
using Mirai.CSharp.HttpApi.Invoking;
using Mirai.CSharp.HttpApi.Models.ChatMessages;
using Mirai.CSharp.HttpApi.Options;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    public static class Program // 前提: nuget Mirai-CSharp, 版本需要 >= 2.0.0
    {
        public static Task Main()
        {
            IServiceCollection sc = new ServiceCollection(); // 如果你使用 ASP.NET Core 相关, 在 Startup 中执行以下方法来注册服务即可
            RegisterServices(sc);
            IServiceProvider services = sc.BuildServiceProvider();
            return ConnectAndWaitExitAsync(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddMiraiBaseFramework()   // 表示使用基于基础框架的构建器
                    .AddHandler<MiraiPlugin>() // 虽然可以把 HttpApiPlugin 作为泛型参数塞入, 但不建议这么做
                    .Services
                    .AddDefaultMiraiHttpFramework() // 表示使用 mirai-api-http 实现的构建器
                    .AddInvoker<MiraiHttpMessageHandlerInvoker>() // 使用默认的调度器
                    .AddHandler<HttpApiPlugin>() // 可以如此添加更多消息处理器
                    .AddClient<MiraiHttpSession>() // 使用默认的客户端
                    .AddParser<DefaultMappableMiraiHttpMessageParser<IGroupMessageEventArgs, GroupMessageEventArgs>>() // 由于 MiraiPlugin 是基于所有平台的消息处理器, 一般不在该类上标注特定平台实现所需用的消息解析器, 所以我们手动添加所需的消息解析器
                    .Services
                    // 由于 MiraiHttpSession 使用 IOptions<MiraiHttpSessionOptions>, 其作为 Singleton 被注册
                    // 配置此项将配置基于此 IServiceProvider 全局的连接配置
                    // 如果你想一个作用域一个配置的话
                    // 需要自行做一个实现类, 继承MiraiHttpSession, 构造参数中使用 IOptionsSnapshot<MiraiHttpSessionOptions>
                    // 并将其传递给父类的构造参数
                    // 然后在每一个作用域中!先!配置好 IOptionsSnapshot<MiraiHttpSessionOptions>, 再尝试获取 IMiraiHttpSession
                    .Configure<MiraiHttpSessionOptions>(options =>
                    {
                        options.Host = "127.0.0.1"; // 主机/IP
                        options.Port = 33111; // 端口
                        options.AuthKey = "yourkey"; // 凭据
                    })
                    .AddLogging();
        }

        public static async Task ConnectAndWaitExitAsync(IServiceProvider services)
        {
            // 由于注册时使用了默认的客户端生命周期:Scoped, 我们需要创建一个IServiceScope
            IServiceScope scope = services.CreateAsyncScope();
            await using var x = (IAsyncDisposable)scope;
            //await using AsyncServiceScope scope = services.CreateAsyncScope(); // 自 .NET 6.0 起才可以如此操作代替上边两句
            services = scope.ServiceProvider;
            // 大部分服务都基于接口注册, 请使用接口作为类型解析
            IMiraiHttpSession session = services.GetRequiredService<IMiraiHttpSession>(); 
            await session.ConnectAsync(0); // 填入期望连接到的机器人QQ号
            while (true)
            {
                // 接下来就万事大吉了, 消息框架会自动实例化消息处理器并在收到相应的消息时调用它们
                if (Console.ReadLine() == "exit")
                {
                    break;
                }
            }
        }
    }
}
```

## 注意事项  
- 本项目使用`C# 9.0`编写, 你需要至少`.NET Core 2.0` 或 `.NET Framework 4.6.1`才能使用本项目, 其中所有的api均为**异步**方法
- 无论是在 `Mirai-CSharp` 或是 `Mirai-CSharp.HttpApi` 中, 各组件的默认生命周期如下:
  1. 调度器(Invoker): Scoped
  2. 处理器(Handler): Scoped
  3. 解析器(Parser): Singleton
  4. 客户端(Client): Scoped

## 使用例子

- [基于任何实现框架的插件](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/MiraiPlugin.cs)
- [基于特定实现框架的插件](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/HttpApiPlugin.cs)
- [对动态增删消息处理器的注释](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/DynamicPlugin.cs)
- [配置Session](https://github.com/Executor-Cheng/Mirai-CSharp/tree/master/Mirai-CSharp.Example/Program.cs)
- [处理好友消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.FriendMessage.cs) 
- [处理群消息](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupMessage.cs)  
- [处理好友申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.NewFriendApply.cs)  
- [处理入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.GroupApply.cs)  
- [处理Bot受邀入群申请](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.BotInvitedJoinGroup.cs)  
- [发送图片](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.SendPicture.cs)  
- [处理Session掉线](https://github.com/Executor-Cheng/Mirai-CSharp/blob/master/Mirai-CSharp.Example/ExamplePlugin.Disconnected.cs)  
