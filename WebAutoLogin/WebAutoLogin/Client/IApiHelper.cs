using System.Collections.Generic;
using System.Net;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Client
{
    public interface IApiHelper
    {
        Account GetAccountById(int id);
        Account GetAccountByToken(string token);
        List<Account> GetAccounts();
        bool HealthCheck();
        Account InsertAccount(Account account);
        HttpStatusCode UpdateAccount(Account account);
    }
}