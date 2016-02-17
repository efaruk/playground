using System;

namespace Practices.ForcerLoop
{
    public static class ForcerLoop
    {
        public delegate void ForcerLoopCalback(ForcerLoopEventArgs args);
        public delegate void ForcerLoopCalback<TResult>(ForcerLoopEventArgs<TResult> args);

        public static void ForceAction(Action action, ForcerLoopPolicy loopPolicy, ForcerLoopCalback callCalback = null)
        {
            var loopBreak = false;
            var failCounter = 1;
            var successStatus = false;
            Exception lastException = null;
            while (!loopBreak)
            {
                try
                {
                    action();
                    successStatus = true;
                }
                catch (Exception ex)
                {
                    failCounter++;
                    lastException = ex;
                    if (!loopPolicy.RetryOnFail)
                    {
                        loopBreak = true;
                    }
                }
                finally
                {
                    if (successStatus)
                    {
                        loopBreak = true;
                    }
                    else
                    {
                        if (failCounter >= loopPolicy.RetryCount)
                        {
                            loopBreak = true;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(loopPolicy.RetryIntervalAsMiliseconds);
                        }
                    }
                }
            }
            if (callCalback != null)
            {
                var eventArgs = new ForcerLoopEventArgs
                {
                    SuccessStatus = successStatus,
                    LastException = lastException
                };
                callCalback(eventArgs);
            }
        }

        public static TResult ForceFunction<TResult>(Func<TResult> function, ForcerLoopPolicy loopPolicy,
            ForcerLoopCalback<TResult> callCalback = null)
        {
            var result = default(TResult);
            var loopBreak = false;
            var failCounter = 1;
            var successStatus = false;
            Exception lastException = null;
            while (!loopBreak)
            {
                try
                {
                    result = function();
                    successStatus = true;
                }
                catch (Exception ex)
                {
                    failCounter++;
                    lastException = ex;
                    if (!loopPolicy.RetryOnFail)
                    {
                        loopBreak = true;
                    }
                }
                finally
                {
                    if (successStatus)
                    {
                        loopBreak = true;
                    }
                    else
                    {
                        if (failCounter >= loopPolicy.RetryCount)
                        {
                            loopBreak = true;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(loopPolicy.RetryIntervalAsMiliseconds);
                        }
                    }
                }
            }
            if (callCalback != null)
            {
                var eventArgs = new ForcerLoopEventArgs<TResult>(result)
                {
                    SuccessStatus = successStatus,
                    LastException = lastException
                };
                callCalback(eventArgs);
            }
            return result;
        }
    }
}