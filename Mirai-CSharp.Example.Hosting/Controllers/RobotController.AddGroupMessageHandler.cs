using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mirai.CSharp.Example.Hosting.Handlers;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example.Hosting.Controllers
{
    public sealed partial class RobotController
    {
        [HttpGet]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddGroupMessageHandlerAsync([Required] int robotQQ, CancellationToken token)
        {
            IMiraiHttpSession session = await _robotManager.RetriveSessionAsync(robotQQ, token);
            session.AddPlugin(new GroupMessageHandler());
            return (JsonResult)ResponseModel.CreateSuccess();
        }
    }
}
