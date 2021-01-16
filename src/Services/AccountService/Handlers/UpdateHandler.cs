using System;
using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast.Abstractions;

namespace src.Services.AccountService.Handlers
{
    public class UpdateHandler : CommandHandler<UpdateAccount, Account>
    {
        protected override Task<Account> Handle(UpdateAccount request)
        {
            throw new NotImplementedException();
        }
    }
}