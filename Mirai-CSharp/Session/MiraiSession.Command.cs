using System;
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
        [Obsolete("新版本的 mirai-console 中已经没有管理员概念了, 参考: https://github.com/project-mirai/mirai-api-http/pull/265#discussion_r598428011")]
        public abstract Task<long[]> GetManagersAsync(long qqNumber, CancellationToken token = default);
    }
}
