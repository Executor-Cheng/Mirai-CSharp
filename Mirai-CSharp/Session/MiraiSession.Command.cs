using System.Threading;
using System.Threading.Tasks;

namespace Mirai.CSharp.Session
{
    public abstract partial class MiraiSession
    {
        /// <inheritdoc/>
        public virtual Task ExecuteCommandAsync(string name, params string[]? args)
        {
            return ExecuteCommandAsync(name, args, default);
        }

        /// <inheritdoc/>
        public abstract Task ExecuteCommandAsync(string name, string[]? args, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task RegisterCommandAsync(string name, string[]? alias = null, string? description = null, string? usage = null, CancellationToken token = default);

        /// <inheritdoc/>
        public abstract Task<long[]> GetManagersAsync(long qqNumber, CancellationToken token = default);
    }
}
