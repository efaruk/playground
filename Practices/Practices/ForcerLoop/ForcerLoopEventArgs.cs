using System;

namespace Practices.ForcerLoop
{
    public class ForcerLoopEventArgs<TResult> : ForcerLoopEventArgs
    {
        public ForcerLoopEventArgs(TResult result)
        {
            Result = result;
        }

        public TResult Result { get; private set; }
    }

    public class ForcerLoopEventArgs : EventArgs
    {
        public bool SuccessStatus { get; set; }
        public Exception LastException { get; set; }
    }
}