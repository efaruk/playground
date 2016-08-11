using System.Collections.Generic;
using System.Net;
using RestSharp;
using WebAutoLogin.Configuration;
using WebAutoLogin.Data.Entities;

namespace WebAutoLogin.Client
{
    public class ApiHelper : IApiHelper
    {
        private readonly Account _account;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable : It can be used later some where else
        private readonly ISettingsProvider _settingsProvider;

        public ApiHelper(Account account, ISettingsProvider settingsProvider)
        {
            _account = account;
            _settingsProvider = settingsProvider;
            _baseUrl = _settingsProvider.GetAppSetting("ApiUrl");
        }

        private string _baseUrl;

        public bool HealthCheck()
        {
            var client = SetupClient();
            var request = new RestRequest("/", Method.GET);
            // AddTokenHeader(request, _token);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public List<Account> GetAccounts()
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.GET);
            // AddTokenHeader(request, _token);
            var response = client.Execute<List<Account>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null) return response.Data;
            }
            return null;
        }

        public Account GetAccountById(int id)
        {
            var client = SetupClient();
            var request = new RestRequest(string.Format("/accounts/{0}", id), Method.GET);
            // AddTokenHeader(request, _token);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null) return response.Data;
            }
            return null;
        }

        public Account GetAccountByToken(string token)
        {
            var client = SetupClient();
            var request = new RestRequest(string.Format("/accounts/token/{0}", token), Method.GET);
            // AddTokenHeader(request, _token);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null) return response.Data;
            }
            return null;
        }

        public void InsertAccount(Account account)
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.POST);
            // AddTokenHeader(request, _token);
            //var parameter = _serializer.Serialize(account);
            request.AddBody(account);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
            }
        }

        public void UpdateAccount(Account account)
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.PUT);
            // AddTokenHeader(request, _token);
            //var parameter = _serializer.Serialize(account);
            request.AddBody(account);
            var response = client.Execute<Account>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
            }
        }

        private RestClient SetupClient()
        {
            var client = new RestClient(_baseUrl);
            if (_account != null)
            {
                client.AddDefaultHeader("token", _account.Token);
            }
            return client;
        }

        //private static void AddTokenHeader(IRestRequest request, string token)
        //{
        //    request.AddHeader("token", token);
        //}

    }
}
