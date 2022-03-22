using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mirai.CSharp.Example.Hosting.Controllers
{
    public sealed partial class RobotController
    {
        [HttpGet]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync([Required]int robotQQ, CancellationToken token)
        {
            await _robotManager.RetriveSessionAsync(robotQQ, token);
            return (JsonResult)ResponseModel.CreateSuccess();
        }
    }
}
