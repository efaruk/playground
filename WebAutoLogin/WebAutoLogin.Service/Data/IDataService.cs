using System;
using System.Collections.Generic;
using System.Data;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Service.Data
{
    public interface IDataService
    {
        void ExecuteQuery(IDbConnection connection, Action action);
        T ExecuteQuery<T>(IDbConnection connection, Func<T> function) where T : class;
        Account Login(string userName, string password);
        Account GetAccountById(int id);
        Account GetAccountByToken(string token);
        List<Account> GetAccounts();
        void InsertAccount(Account account);
        void UpdateAccount(Account account);
    }
}