using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using WebAutoLogin.Configuration;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Service.Data
{
    public class DataService : IDataService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IDbConnection _dbConnection;

        public DataService(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            _dbConnection = new SQLiteConnection(settingsProvider.GetConnectionString("ServiceDatabase"));
        }


        public DataService(IDbConnection dbConnection, ISettingsProvider settingsProvider)
        {
            _dbConnection = dbConnection;
            _settingsProvider = settingsProvider;
        }

        public Account Login(string userName, string password)
        {
            Account account = null;
            ExecuteQuery(_dbConnection, () =>
            {
                account = _dbConnection.Query<Account>(
                "select Id, FullName, UserName, Password, Token, Locked, Admin from Account where UserName=@UserName and Password=@Password",
                new { UserName = userName, Password = password }).FirstOrDefault();
            });
            return account;
        }

        public Account GetAccountByToken(string token)
        {
            Account account = null;
            ExecuteQuery(_dbConnection, () =>
            {
                account = _dbConnection.Query<Account>(
                "select Id, FullName, UserName, Password, Token, Locked, Admin from Account where Token=@Token",
                new { Token = token }).FirstOrDefault();
            });
            return account;
        }

        public Account GetAccountById(int id)
        {
            var account = new Account();
            ExecuteQuery(_dbConnection, () =>
            {
                account = _dbConnection.Query<Account>(
                "select Id, FullName, UserName, Password, Token, Locked, Admin from Account where Id=@Id",
                new { Id = id }).FirstOrDefault();
            });
            return account;
        }

        public Account InsertAccount(Account account)
        {
            ExecuteQuery(_dbConnection, () =>
            {
                var query = "insert into Account(FullName, UserName, Password, Token, Locked, Admin)" +
                            "values ((@FullName, @UserName, @Password, @Token, @Locked, @Admin))" +
                            "SELECT last_insert_rowid();";
                var id = _dbConnection.Query<int>(query, account);

                account = _dbConnection.Query<Account>(
                "select Id, FullName, UserName, Password, Token, Locked, Admin from Account where Id=@Id",
                new { Id = id }).FirstOrDefault();
            });
            return account;
        }

        public void UpdateAccount(Account account)
        {
            ExecuteQuery(_dbConnection, () =>
            {
                var query =
                    "update Account set FullName=@FullName, UserName=@UserName, Password=@Password, Token=@Token, Locked=@Locked, Admin=@Admin where Id=@Id";
                _dbConnection.Execute(query, account);
            });
        }

        public List<Account> GetAccounts()
        {
            var accounts = new List<Account>();
            ExecuteQuery(_dbConnection, () =>
            {
                accounts = _dbConnection.Query<Account>(
                "select Id, FullName, UserName, Password, Token, Locked, Admin from Account").ToList();
            });
            return accounts;
        }

        public void ExecuteQuery(IDbConnection connection, Action action)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            action();
            connection.Close();
        }

        public T ExecuteQuery<T>(IDbConnection connection, Func<T> function) where T: class
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var t = function();
            connection.Close();
            return t;
        }
    }
}