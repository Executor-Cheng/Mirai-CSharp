using System.Runtime.CompilerServices;
using System.Threading;

namespace Mirai.CSharp.Utility
{
    /// <summary>
    /// 读写互斥锁。由符号位表示写锁, 其余位表示读锁
    /// </summary>
    /// <remarks>
    /// 不正确地使用本锁将导致方法永远不会返回
    /// </remarks>
    public struct UInt32Lock
    {
        private uint _lock;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint CompareExchange(ref uint location1, uint value, uint comparand)
        {
#if !NET5_0_OR_GREATER
            return (uint)Interlocked.CompareExchange(ref Unsafe.As<uint, int>(ref location1), (int)value, (int)comparand);
#else
            return Interlocked.CompareExchange(ref location1, value, comparand);
#endif
        }

        public void EnterWriteLock()
        {
            uint lastLock;
            do
            {
                // 不允许其它线程持有写锁以及任何一个读锁
                while ((lastLock = _lock) != 0)
                {

                }
            }
            while (CompareExchange(ref _lock, 0x80000000, lastLock) != lastLock);
        }

        public void ExitWriteLock()
        {
            Volatile.Write(ref _lock, 0u);
        }

        public void EnterReadLock()
        {
            uint lastLock, currentLock;
            do
            {
                // 不允许其它线程持有写锁
                while (((lastLock = _lock) >> 31) != 0)
                {

                }
                currentLock = lastLock + 1;
            }
            while (CompareExchange(ref _lock, currentLock, lastLock) != lastLock);
        }

        public void ExitReadLock()
        {
            uint lastLock;
            do
            {
                lastLock = _lock;
            }
            while (CompareExchange(ref _lock, lastLock - 1, lastLock) != lastLock);
        }
    }
}
