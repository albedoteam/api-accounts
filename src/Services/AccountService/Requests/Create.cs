using Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace Accounts.Api.Services.AccountService.Requests
{
    public class Create : IRequest<Result<Account>>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}