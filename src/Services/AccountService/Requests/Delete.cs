namespace Accounts.Api.Services.AccountService.Requests
{
    using AlbedoTeam.Sdk.FailFast;
    using MediatR;
    using Models;

    public class Delete : IRequest<Result<Account>>
    {
        public string Id { get; set; }
    }
}