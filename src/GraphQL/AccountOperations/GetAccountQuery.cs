using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;

namespace Accounts.Api.GraphQL.AccountOperations
{
    public class GetAccountQuery
    {
        private readonly IRequestClient<GetAccount> _client;
        private readonly IAccountMapper _mapper;

        public GetAccountQuery(IRequestClient<GetAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Models.Account> GetAccount(Get request)
        {
            var (successResponse, errorResponse) =
                await _client.GetResponse<AccountResponse, ErrorResponse>(_mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
                return null;

            var account = (await successResponse).Message;
            return _mapper.MapResponseToModel(account);
        }
    }
}