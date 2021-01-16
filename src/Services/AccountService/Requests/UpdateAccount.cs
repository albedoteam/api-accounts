using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Sdk.FailFast;
using MediatR;

namespace AlbedoTeam.Accounts.Api.Services.AccountService.Requests
{
    public class UpdateAccount : IRequest<Response<Account>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}