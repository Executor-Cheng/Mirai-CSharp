using Mirai_CSharp_Test_efw561we5fwef65.Models;
using System;
using System.Threading.Tasks;

#pragma warning disable CA1031 // Do not catch general exception types
namespace Mirai_CSharp_Test_efw561we5fwef65.Example
{
    public partial class ExamplePlugin
    {
        public async Task<bool> Disconnected(MiraiHttpSession session, IDisconnectedEventArgs e)
        {
            // e.Exception: 引发掉线的响应异常, 按需处理
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions("127.0.0.1", 33111, "8d726307dd7b468d8550a95f236444f7");
            while (true)
            {
                try
                {
                    await session.ConnectAsync(options, 0); // 连到成功为止, QQ号自填, 你也可以另行处理重连的 behaviour
                    return true;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
