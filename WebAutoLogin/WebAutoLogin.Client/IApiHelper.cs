using System.Collections.Generic;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Client
{
    public interface IApiHelper
    {
        Account GetAccountById(int id);
        Account GetAccountByToken(string token);
        List<Account> GetAccounts();
        bool HealthCheck();
        void InsertAccount(Account account);
        void UpdateAccount(Account account);
    }
}