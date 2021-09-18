namespace Accounts.Api.Services.AccountService.Handlers
{
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AlbedoTeam.Sdk.FailFast;
    using AlbedoTeam.Sdk.FailFast.Abstractions;
    using Extensions;
    using Mappers.Abstractions;
    using MassTransit;
    using Models;
    using Requests;

    public class GetHandler : QueryHandler<Get, Account>
    {
        private readonly IRequestClient<GetAccount> _client;
        private readonly IAccountMapper _mapper;

        public GetHandler(IRequestClient<GetAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Result<Account>> Handle(Get request)
        {
            var (successResponse, errorResponse) =
                await _client.GetResponse<AccountResponse, ErrorResponse>(_mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return await errorResponse.Parse<Account>();

            var account = (await successResponse).Message;
            return new Result<Account>(_mapper.MapResponseToModel(account));
        }
    }
}