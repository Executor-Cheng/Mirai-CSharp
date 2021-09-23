using System.Threading;
using System.Threading.Tasks;
using Mirai.CSharp.Models;

namespace Mirai.CSharp.Session
{
    public partial class MiraiSession
    {
        /// <inheritdoc/>
        public abstract Task NudgeAsync(NudgeTarget target, long qqNumber, long? groupNumber = null, CancellationToken token = default);
    }
}
