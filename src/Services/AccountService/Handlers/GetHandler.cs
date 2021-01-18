using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Requests;
using Accounts.Responses;
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

        protected override async Task<Account> Handle(Get request)
        {
            var (accountResponse, notFoundResponse) =
                await _client.GetResponse<AccountResponse, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (accountResponse.IsCompletedSuccessfully)
            {
                var item = await accountResponse;
                return _mapper.MapResponseToModel(item.Message);
            }

            await notFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}