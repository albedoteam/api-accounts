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
    public class DeleteHandler : CommandHandler<DeleteAccount, Account>
    {
        private readonly IRequestClient<DeleteAccountRequest> _client;
        private readonly IAccountMapper _mapper;

        public DeleteHandler(IRequestClient<DeleteAccountRequest> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(DeleteAccount request)
        {
            var (accountDeletedResponse, accountNotFoundResponse) =
                await _client.GetResponse<AccountDeleted, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (accountDeletedResponse.IsCompletedSuccessfully)
            {
                var accountDeleted = (await accountDeletedResponse).Message;
                return _mapper.MapResponseToModel(accountDeleted);
            }

            await accountNotFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}