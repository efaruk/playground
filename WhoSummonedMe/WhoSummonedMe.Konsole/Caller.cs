using System;

namespace WhoSummonedMe.Konsole
{
    public class Caller : IDisposable, ICaller
    {

        public void Call()
        {
            CheckDisposed();
            var target = Summoner.Summon<Target>(this);
            target.Test();
        }

        private bool _disposed;
        public void Dispose() { _disposed = true; }

        public override string ToString()
        {
            CheckDisposed();
            return base.ToString();
        }

        private void CheckDisposed()
        {
            if (_disposed) throw new ObjectDisposedException("This object is disposed...");
        }

        public bool IsDisposed
        {
            get { return _disposed; }
        }
    }
}
