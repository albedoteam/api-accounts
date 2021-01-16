using Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class CreateAccount : IRequest<Response<Account>>
    {
        public string Name { get; set; }
    }
}