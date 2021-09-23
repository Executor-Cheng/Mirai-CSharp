using System;
using System.Threading.Tasks;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example
{
    // IDisconnectedEventArgs 和 IUnknownMessageEventArgs 不需要标定 RegisterMiraiHttpParserAttribute
    public partial class ExamplePlugin : IMiraiHttpMessageHandler<IDisconnectedEventArgs>
    {
        public async Task HandleMessageAsync(IMiraiHttpSession session, IDisconnectedEventArgs e)
        {
            // e.Exception: 引发掉线的响应异常, 按需处理
            while (true)
            {
                try
                {
                    await session.ConnectAsync(0); // 连到成功为止, QQ号自填, 你也可以另行处理重连的 behaviour
                    e.BlockRemainingHandlers = true;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
