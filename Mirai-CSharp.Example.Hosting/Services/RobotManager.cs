using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mirai.CSharp.HttpApi.Session;

namespace Mirai.CSharp.Example.Hosting.Services
{
    public sealed class RobotManager
    {
        private readonly IServiceProvider _services;

        private readonly ConcurrentDictionary<long, IServiceScope> _robotScopes;

        private readonly ConcurrentDictionary<long, TaskCompletionSource<Task<IMiraiHttpSession>>> _robotInitTasks;

        public RobotManager(IServiceProvider services)
        {
            _services = services;
            _robotScopes = new ConcurrentDictionary<long, IServiceScope>();
            _robotInitTasks = new ConcurrentDictionary<long, TaskCompletionSource<Task<IMiraiHttpSession>>>();
        }

        public ValueTask<IMiraiHttpSession> RetriveSessionAsync(long robotQQ, CancellationToken token = default)
        {
            while (true)
            {
                if (_robotScopes.TryGetValue(robotQQ, out IServiceScope? session))
                {
                    return new ValueTask<IMiraiHttpSession>(session.ServiceProvider.GetRequiredService<IMiraiHttpSession>());
                }
                if (_robotInitTasks.TryGetValue(robotQQ, out TaskCompletionSource<Task<IMiraiHttpSession>>? actualTcs))
                {
                    return new ValueTask<IMiraiHttpSession>(actualTcs.Task.Unwrap());
                }
                TaskCompletionSource<Task<IMiraiHttpSession>> createdTcs = new TaskCompletionSource<Task<IMiraiHttpSession>>();
                if (!_robotInitTasks.TryAdd(robotQQ, createdTcs))
                {
                    continue;
                }
                Task<IMiraiHttpSession> task = InternalRetriveSessionAsync(robotQQ, token);
                createdTcs.SetResult(task);
                return new ValueTask<IMiraiHttpSession>(task);
            }
        }

        private async Task<IMiraiHttpSession> InternalRetriveSessionAsync(long robotQQ, CancellationToken token)
        {
            try
            {
                if (_robotScopes.TryGetValue(robotQQ, out IServiceScope? dualcheck))
                {
                    return dualcheck.ServiceProvider.GetRequiredService<IMiraiHttpSession>();
                }
                IServiceScope scope = _services.CreateScope();
                try
                {
                    IMiraiHttpSession session = scope.ServiceProvider.GetRequiredService<IMiraiHttpSession>();
                    await session.ConnectAsync(robotQQ, token).ConfigureAwait(false);
                    _robotScopes[robotQQ] = scope;
                    return session;
                }
                catch
                {
                    scope.Dispose();
                    throw;
                }
            }
            finally
            {
                _robotInitTasks.TryRemove(robotQQ, out _);
            }
        }

        public bool RemoveSession(long robotQQ)
        {
            if (!_robotScopes.TryRemove(robotQQ, out IServiceScope? scope))
            {
                return false;
            }
            scope.Dispose();
            return true;
        }
    }
}
