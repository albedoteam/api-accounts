using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Requests
{
    public class GetAccount : IRequest<Response<Account>>
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}