using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Requests
{
    public class DeleteAccount : IRequest<Response<Account>>
    {
        public string Id { get; set; }
    }
}