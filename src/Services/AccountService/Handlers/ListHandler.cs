using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Sdk.FailFast.Abstractions;

namespace src.Services.AccountService.Handlers
{
    public class ListHandler : QueryHandler<ListAccounts, List<Account>>
    {
        protected override Task<List<Account>> Handle(ListAccounts request)
        {
            throw new NotImplementedException();
        }
    }
}