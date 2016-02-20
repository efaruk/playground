namespace Practices.Impersonation
{
    public class ImpersonatorUsage
    {
        public void Usage()
        {
            Impersonator.RunImpersonatedAction("domain.local", "username", "password", () =>
            {
                //Do something here with account username@domain.local
            });
        }
    }
}