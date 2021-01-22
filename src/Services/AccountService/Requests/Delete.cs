using Accounts.Api.Models;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class Delete : IRequest<Result<Account>>, DeleteAccount
    {
        public string Id { get; set; }
    }
}