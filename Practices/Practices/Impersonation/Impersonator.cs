using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;

namespace Practices.Impersonation
{
    public static class Impersonator
    {
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        [DllImport("advapi32.dll")]
        private static extern int LogonUserAs(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        public static void RunImpersonatedAction(string domain, string userName, string password, Action action)
        {
            WindowsImpersonationContext impersonationContext = ImpersonateValidUser(userName, password, domain);

            if (impersonationContext != null)
            {
                try
                {
                    if (action != null)
                    {
                        action();
                    }
                    else
                    {
                        throw new ApplicationException("Impersonation Failed: Action can not be null.");
                    }
                }
                finally
                {
                    UndoImpersonation(impersonationContext);
                }
            }
            else
            {
                throw new SecurityException("Impersonation Logon Falied.");
            }
        }

        private static WindowsImpersonationContext ImpersonateValidUser(string userName, string password, string domain)
        {
            WindowsImpersonationContext impersonationContext;
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserAs(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        CloseHandle(token);
                        CloseHandle(tokenDuplicate);
                        return impersonationContext;
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return null;
        }

        private static void UndoImpersonation(WindowsImpersonationContext impersonationContext)
        {
            if (impersonationContext != null)
                impersonationContext.Undo();
        }
    }
}