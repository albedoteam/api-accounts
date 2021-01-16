using System;
using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast.Abstractions;

namespace src.Services.AccountService.Handlers
{
    public class CreateHandler : CommandHandler<CreateAccount, Account>
    {
        protected override Task<Account> Handle(CreateAccount request)
        {
            throw new NotImplementedException();
        }
    }
}