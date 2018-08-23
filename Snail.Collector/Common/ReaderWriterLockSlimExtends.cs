using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Collector.Common
{
    public static class ReaderWriterLockSlimExtends
    {
        public static T SafeReadValue<T>(this ReaderWriterLockSlim lockSlim, Func<T> readFunc)
        {
            try
            {
                lockSlim.EnterReadLock();
                return readFunc();
            }
            finally
            {
                if (lockSlim.IsReadLockHeld)
                {
                    lockSlim.ExitReadLock();
                }
            }
        }

        public static void SafeSetValue<T>(this ReaderWriterLockSlim lockSlim, Action<T> setAction, T value)
        {
            try
            {
                lockSlim.EnterWriteLock();
                setAction(value);
            }
            finally
            {
                if (lockSlim.IsWriteLockHeld)
                {
                    lockSlim.ExitWriteLock();
                }
            }
        }

        public static TResult SafeSetValue<T,TResult>(this ReaderWriterLockSlim lockSlim, Func<T,TResult> setFunc, T value)
        {
            try
            {
                lockSlim.EnterWriteLock();
                return setFunc(value);
            }
            finally
            {
                if (lockSlim.IsWriteLockHeld)
                {
                    lockSlim.ExitWriteLock();
                }
            }
        }
    }
}
