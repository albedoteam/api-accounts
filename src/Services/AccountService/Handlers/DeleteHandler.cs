namespace Accounts.Api.Services.AccountService.Handlers
{
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Events;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AlbedoTeam.Sdk.FailFast;
    using AlbedoTeam.Sdk.FailFast.Abstractions;
    using Extensions;
    using Mappers.Abstractions;
    using MassTransit;
    using Models;
    using Requests;

    public class DeleteHandler : CommandHandler<Delete, Account>
    {
        private readonly IRequestClient<DeleteAccount> _client;
        private readonly IAccountMapper _mapper;

        public DeleteHandler(IRequestClient<DeleteAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<Account>> Handle(Delete request)
        {
            var (successResponse, errorResponse) =
                await _client.GetResponse<AccountDeleted, ErrorResponse>(_mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return await errorResponse.Parse<Account>();

            var accountDeleted = (await successResponse).Message;
            return new Result<Account>(_mapper.MapResponseToModel(accountDeleted));
        }
    }
}