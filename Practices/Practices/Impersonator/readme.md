How to imersonate process: Run process with different user account.

Impersonator is implementation to run any process with different user account. Please check Impersonator.cs

Usage: ImpersonatorUsage.cs

```csharp

public void Usage()
{
    Impersonator.RunImpersonatedAction("domain.local", "username", "password", () =>
    {
        //Do something here with account username@domain.local
    });
}

```