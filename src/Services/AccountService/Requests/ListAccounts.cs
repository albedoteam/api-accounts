using System.Collections.Generic;
using Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class ListAccounts : IRequest<Response<List<Account>>>
    {
        public bool ShowDeleted { get; set; }
    }
}