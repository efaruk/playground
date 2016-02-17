using System;
using System.Threading;

namespace Practices.AsyncUsing
{
    public class AsyncUsingUsage
    {
        public async void Usage()
        {
            var asyncUsing = new AsyncUsing<UsageClass>(() => { return new UsageClass(); }, usage => { usage.Sleep(3); },
                usage => { usage.Dispose(); });
            await asyncUsing.Start();
        }
    }

    public class UsageClass: IDisposable
    {
        public void Sleep(int second)
        {
            Thread.Sleep(second * 1000);
        }

        public void Dispose()
        {
            //Dispose some resource
        }
    }
}