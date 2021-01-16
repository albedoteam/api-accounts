using Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class UpdateAccount : IRequest<Response<Account>>
    {
        public string Id { get; set; }
    }
}