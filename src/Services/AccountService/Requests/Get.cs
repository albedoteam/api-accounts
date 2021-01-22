using Accounts.Api.Models;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class Get : IRequest<Result<Account>>, GetAccount
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}