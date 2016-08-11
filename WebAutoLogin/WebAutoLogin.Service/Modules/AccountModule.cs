using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using WebAutoLogin.Data.Entities;
using WebAutoLogin.Service.Services;

namespace WebAutoLogin.Service.Modules
{
    public class AccountModule : NancyModule
    {
        public AccountModule(IApiService apiService) : base("/accounts")
        {
            Get["/"] = p =>
            {
                var accounts = apiService.GetAccounts();
                return Response.AsJson(accounts);
            };

            Get["/token/{token}"] = p =>
            {
                var account = apiService.GetAccountByToken(p.token);
                if (account != null)
                {
                    return Response.AsJson((Account) account);
                }
                return Negotiate.WithStatusCode(HttpStatusCode.Forbidden);
            };

            //Get["/login/{username}/{password}"] = p =>
            //{
            //    var account = apiService.Login(p.username, p.password);
            //    return Response.AsJson((Account)account);
            //};

            Get["/{id:int}"] = p =>
            {
                var account = apiService.GetAccountById(p.id);
                return Response.AsJson((Account)account);
            };

            Post["/"] = p =>
            {
                var account = this.Bind<Account>();
                apiService.InsertAccount(account);
                return Response.AsText("OK");
            };

            Put["/"] = p =>
            {
                var account = this.Bind<Account>();
                apiService.UpdateAccount(account);
                return Response.AsText("OK");
            };

            Delete["/{id:int}"] = p =>
            {
                var response = new Response { StatusCode = HttpStatusCode.Forbidden };
                return response;
            };

        }


    }
}