How to force some action/function to success with specific policy like "try 100 times, retry interval 3 seconds"

The answer is ForcerLoop :alien:

Usage: ForcerLoopUsage.cs

```csharp

public void UsageAction()
{
    ForcerLoop.ForceAction(() =>
    {
        //Do something here...
    }, new ForcerLoopPolicy {RetryCount = 1000, RetryIntervalAsSeconds = 3, RetryOnFail = true }, args =>
    {
        if (!args.SuccessStatus)
        {
            throw args.LastException;
        }
    });
}

public int UsageFunction()
{
    var rc = 0;
    ForcerLoop.ForceFunction(() =>
    {
        //Do something here...
        return 1;
    }, new ForcerLoopPolicy {RetryCount = 1000, RetryIntervalAsSeconds = 3, RetryOnFail = true}, args =>
    {
        rc = args.Result;
        if (args.Result == 0 && !args.SuccessStatus)
        {
            throw args.LastException;
        }
    });
    return rc;
}

```