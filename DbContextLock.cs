using System;
using System.Threading;

namespace Abeslamidze_Kursovaya7
{
    public class DbContextLock : IDisposable
    {
        private readonly object lockObject = new object();
        private bool isLocked = false;

        public void Lock()
        {
            Monitor.Enter(lockObject);
            isLocked = true;
        }

        public void Unlock()
        {
            Monitor.Exit(lockObject);
            isLocked = false;
        }

        public bool IsLocked => isLocked;

        public void Dispose()
        {
            if (isLocked)
            {
                Unlock();
            }
        }
    }
}

