using Mirai.CSharp.Models;
using System;
using System.Threading.Tasks;

namespace Mirai.CSharp.Example
{
    public static class Program // 前提: nuget Mirai-CSharp, 版本需要 >= 1.0.1.0
    {
        public static async Task Main()
        {
            // 把你要连接到的 mirai-api-http 所需的主机名/IP, 端口 和 AuthKey 全部填好
            // !! 最好不要用我例子里边的 key 和 端口, 请自己生成一个, 比如 System.Guid.NewGuid().ToString("n") !!
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions("127.0.0.1", 33111, "8d726307dd7b468d8550a95f236444f7");
            // session 使用 DisposeAsync 模式, 所以使用 await using 自动调用 DisposeAsync 方法。
            // 你也可以不在这里 await using, 不过使用完 session 后请务必调用 DisposeAsync 方法
            await using MiraiHttpSession session = new MiraiHttpSession();
            // 把你实现了 Mirai_CSharp.Plugin.Interfaces 下的接口的类给 new 出来, 然后作为插件塞给 session
            ExamplePlugin plugin = new ExamplePlugin();
            // 你也可以一个个绑定事件。比如 session.GroupMessageEvt += plugin.GroupMessage;
            // 手动绑定事件后不要再调用AddPlugin, 否则可能导致重复调用
            session.AddPlugin(plugin);
            // 使用上边提供的信息异步连接到 mirai-api-http
            await session.ConnectAsync(options, 0); // 自己填机器人QQ号
            while (true)
            {
                if (await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}
