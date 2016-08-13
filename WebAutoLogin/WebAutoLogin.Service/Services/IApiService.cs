using System.Collections.Generic;
using System.Net;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Service.Services
{
    public interface IApiService
    {
        Account Login(string userName, string password);
        Account GetAccountByToken(string token);
        Account GetAccountById(int id);
        List<Account> GetAccounts();
        Account InsertAccount(Account account);
        void UpdateAccount(Account account);
        HttpStatusCode DeleteAccount(int id);
    }
}