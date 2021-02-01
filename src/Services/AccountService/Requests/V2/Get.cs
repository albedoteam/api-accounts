using Accounts.Api.Models.V2;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests.V2
{
    public class Get : IRequest<Result<Account>>, GetAccount
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}