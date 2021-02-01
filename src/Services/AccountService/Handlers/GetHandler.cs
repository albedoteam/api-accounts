using System.Threading.Tasks;
using Accounts.Api.Extensions;
using Accounts.Api.Mappers;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using AlbedoTeam.Sdk.FailFast;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers
{
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