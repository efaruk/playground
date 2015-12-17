using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
// ReSharper disable InconsistentlySynchronizedField :Because ConcurrentDictionary used...

namespace WhoSummonedMe
{
    public static class Summoner
    {
        private static readonly Timer _cleanTimer = new Timer(1000);

        static Summoner() {
            _cleanTimer.Elapsed += CleanTimerElapsed;
            _cleanTimer.Start();
        }

        private static bool _inPeriod;
        private static void CleanTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_inPeriod) return;
            _inPeriod = true;
            try
            {
                Clean();
            }
            finally
            {
                _inPeriod = false;
            }
        }


        private static readonly object CleanLock = new object();
        private static void Clean()
        {
            lock (CleanLock)
            {
                foreach (var o in Callers.Where(o => o.Value.IsDisposed))
                {
                    ICaller caller;
                    while (Callers.TryRemove(o.Key, out caller))
                    {
                    }
                }
            }
        }


        private static readonly ConcurrentDictionary<Guid, ICaller> Callers = new ConcurrentDictionary<Guid, ICaller>();
        public static T Summon<T>(ICaller caller)
        {
            var task = Task.Run(() =>
            {
                var guid = Guid.NewGuid();
                while (Callers.TryAdd(guid, caller))
                {
                }
            });
            var t = Activator.CreateInstance<T>();
            return t;
        }

        public static int LiveCallerCount()
        {
            var c = Callers.Count;
            return c;
        }

    }


}
