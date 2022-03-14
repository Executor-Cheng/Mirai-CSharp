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
                    await session.ConnectAsync(e.LastConnectedQQNumber);
                    e.BlockRemainingHandlers = true;
                    break;
                }
                catch (ObjectDisposedException) // session 已被释放
                {
                    break;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
