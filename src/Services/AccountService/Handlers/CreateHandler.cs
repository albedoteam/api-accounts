using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Requests;
using Accounts.Responses;
using AlbedoTeam.Sdk.ExceptionHandler.Exceptions;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers
{
    public class CreateHandler : CommandHandler<Create, Account>
    {
        private readonly IRequestClient<CreateAccount> _client;
        private readonly IAccountMapper _mapper;

        public CreateHandler(IRequestClient<CreateAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(Create request)
        {
            var (accountResponse, accountExistsResponse) =
                await _client.GetResponse<AccountResponse, AccountExists>(_mapper.MapRequestToBroker(request));

            if (accountExistsResponse.IsCompletedSuccessfully)
            {
                await accountExistsResponse;
                throw new ResourceExistsException();
            }

            var account = await accountResponse;
            return _mapper.MapResponseToModel(account.Message);
        }
    }
}