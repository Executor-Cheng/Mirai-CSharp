using System.Threading;
using System.Threading.Tasks;

namespace Mirai_CSharp.Session
{
    public abstract partial class MiraiSession
    {
        public abstract Task ExecuteCommandAsync(string name, string[]? args, CancellationToken token = default);

        public abstract Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default);

        public abstract Task<long[]> GetManagersAsync(long qqNumber, CancellationToken token = default);
    }
}
