namespace Accounts.Api.Services.AccountService.Requests
{
    using AlbedoTeam.Sdk.FailFast;
    using MediatR;
    using Models;

    // [Cache(7200)]
    public class Get : IRequest<Result<Account>>
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
        public bool NoCache { get; set; }
    }
}