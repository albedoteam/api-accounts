using System.Collections.Generic;
using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Requests
{
    public class ListAccounts : IRequest<Response<List<Account>>>
    {
        public bool ShowDeleted { get; set; }
    }
}