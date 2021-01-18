using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Events;
using Accounts.Requests;
using Accounts.Responses;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers
{
    public class UpdateHandler : CommandHandler<Update, Account>
    {
        private readonly IRequestClient<UpdateAccount> _client;
        private readonly IAccountMapper _mapper;

        public UpdateHandler(IRequestClient<UpdateAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(Update request)
        {
            var (accountUpdatedResponse, accountNotFoundResponse) =
                await _client.GetResponse<AccountUpdated, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (accountUpdatedResponse.IsCompletedSuccessfully)
            {
                var accountUpdated = (await accountUpdatedResponse).Message;
                return _mapper.MapResponseToModel(accountUpdated);
            }

            await accountNotFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}