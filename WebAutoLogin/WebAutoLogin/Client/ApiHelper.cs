using System.Collections.Generic;
using System.Net;
using RestSharp;
using WebAutoLogin.Configuration;
using WebAutoLogin.Data.Entities;
//using RestRequest = RestSharp.Newtonsoft.Json.RestRequest;

namespace WebAutoLogin.Client
{
    public class ApiHelper : IApiHelper
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable : It can be used later some where else
        private readonly ISettingsProvider _settingsProvider;

        public ApiHelper(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            _baseUrl = _settingsProvider.GetAppSetting("ApiUrl");
        }

        private string _baseUrl;

        public bool HealthCheck()
        {
            var client = SetupClient();
            var request = new RestRequest("/", Method.GET);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public List<Account> GetAccounts()
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.GET);
            var response = client.Execute<List<Account>>(request);
            return response.StatusCode != HttpStatusCode.OK ? null : response.Data;
        }

        public Account GetAccountById(int id)
        {
            var client = SetupClient();
            var request = new RestRequest(string.Format("/accounts/{0}", id), Method.GET);
            var response = client.Execute<Account>(request);
            return response.StatusCode != HttpStatusCode.OK ? null : response.Data;
        }

        public Account GetAccountByToken(string token)
        {
            var client = SetupClient();
            var request = new RestRequest(string.Format("/accounts/token/{0}", token), Method.GET);
            var response = client.Execute<Account>(request);
            return response.StatusCode != HttpStatusCode.OK ? null : response.Data;
        }

        public HttpStatusCode InsertAccount(Account account)
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.POST);
            request.AddBody(account);
            var response = client.Execute<Account>(request);
            return response.StatusCode;
        }

        public HttpStatusCode UpdateAccount(Account account)
        {
            var client = SetupClient();
            var request = new RestRequest("/accounts/", Method.PUT);
            request.AddBody(account);
            var response = client.Execute<Account>(request);
            return response.StatusCode;
        }

        private RestClient SetupClient()
        {
            var client = new RestClient(_baseUrl);
            if (GlobalModule.Account != null)
            {
                client.AddDefaultHeader(GlobalModule.TokenHeaderKey, GlobalModule.Account.Token);
            }
            return client;
        }

    }
}
