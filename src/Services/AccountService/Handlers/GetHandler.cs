using System;
using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast.Abstractions;

namespace src.Services.AccountService.Handlers
{
    public class GetHandler : QueryHandler<GetAccount, Account>
    {
        protected override Task<Account> Handle(GetAccount request)
        {
            throw new NotImplementedException();
        }
    }
}