using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mirai.CSharp.HttpApi.Handlers;
using Mirai.CSharp.HttpApi.Models.EventArgs;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example.Hosting.Handlers
{
    public sealed class DefaultDisconnectedHandler : IMiraiHttpMessageHandler<IDisconnectedEventArgs>
    {
        private readonly ILogger<DefaultDisconnectedHandler> _logger;

        public DefaultDisconnectedHandler(ILogger<DefaultDisconnectedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleMessageAsync(IMiraiHttpSession session, IDisconnectedEventArgs message)
        {
            _logger.LogWarning(message.Exception, $"连接被断开。上一次连接的QQ号为:{message.LastConnectedQQNumber}");
            while (!session.Connected) // 防止以前的handler未能正确阻断消息传递
            {
                try
                {
                    await session.ConnectAsync(message.LastConnectedQQNumber);
                    message.BlockRemainingHandlers = true;
                    _logger.LogInformation($"重连成功");
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"重连失败。上一次连接的QQ号为:{message.LastConnectedQQNumber}");
                    await Task.Delay(1000);
                }
            }
        }
    }
}
