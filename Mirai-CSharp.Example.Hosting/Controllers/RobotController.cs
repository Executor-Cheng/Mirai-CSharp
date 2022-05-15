using Microsoft.AspNetCore.Mvc;
using Mirai.CSharp.Example.Hosting.Services;

namespace Mirai.CSharp.Example.Hosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed partial class RobotController : ControllerBase
    {
        private readonly RobotManager _robotManager;

        public RobotController(RobotManager robotManager)
        {
            _robotManager = robotManager;
        }
    }
}
