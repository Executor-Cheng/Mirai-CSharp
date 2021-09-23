using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mirai.CSharp.Extensions
{
    public static class TaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task DisposeWhenCompleted(this Task task, IDisposable? disposable)
        {
            if (disposable == null)
            {
                return task;
            }
            static async Task Await(Task task, IDisposable disposable)
            {
                try
                {
                    await task.ConfigureAwait(false);
                }
                finally
                {
                    disposable.Dispose();
                }
            }
            return Await(task, disposable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> DisposeWhenCompleted<T>(this Task<T> task, IDisposable? disposable)
        {
            if (disposable == null)
            {
                return task;
            }
            static async Task<T> Await(Task<T> task, IDisposable disposable)
            {
                try
                {
                    return await task.ConfigureAwait(false);
                }
                finally
                {
                    disposable.Dispose();
                }
            }
            return Await(task, disposable);
        }
    }
}
