using System.Collections.Generic;
using WebAutoLogin.Client;
using WebAutoLogin.Configuration;
using WebAutoLogin.Data.Entities;
using WebAutoLogin.Security.Cryptography;
using WebAutoLogin.Service.Data;

namespace WebAutoLogin.Service.Services
{
    public class ApiService : IApiService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IDataService _dataService;
        private readonly IEncryptionService _encryptionService;

        public ApiService(ISettingsProvider settingsProvider, IDataService dataService, IEncryptionService encryptionService)
        {
            _settingsProvider = settingsProvider;

            _dataService = dataService;
            _encryptionService = encryptionService;
        }

        public Account Login(string userName, string password)
        {
            var cipher = _encryptionService.Encrypt(password, _settingsProvider.GetAppSetting(GlobalModule.SettingKey),
                _settingsProvider.GetAppSetting(GlobalModule.SettingVector));
            var account = _dataService.Login(userName, cipher);
            return account;
        }

        public Account GetAccountByToken(string token)
        {
            var account = _dataService.GetAccountByToken(token);
            return account;
        }

        public Account GetAccountById(int id)
        {
            var account = _dataService.GetAccountById(id);
            return account;
        }

        public List<Account> GetAccounts()
        {
            var accounts = _dataService.GetAccounts();
            return accounts;
        }

        public void InsertAccount(Account account)
        {

            _dataService.InsertAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            _dataService.UpdateAccount(account);
        }
    }
}
