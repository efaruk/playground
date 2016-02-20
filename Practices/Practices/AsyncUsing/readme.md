When you write an async api and need to manage some async usage resorces you need "async using" :bow:

Usage: AsyncUsingUsage.cs

```csharp

public async void Usage()
{
    var asyncUsing = new AsyncUsing<UsageClass>(() => { return new UsageClass(); }, usage => { usage.Sleep(3); },
        usage => { usage.Dispose(); });
    await asyncUsing.Start();
}

```