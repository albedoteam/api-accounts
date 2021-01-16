using System.Threading.Tasks;
using Accounts.Api._Broker.Events;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
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
            var (todoDeletedResponse, todoNotFoundResponse) =
                await _client.GetResponse<AccountDeleted, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (todoDeletedResponse.IsCompletedSuccessfully)
            {
                var todoDeleted = (await todoDeletedResponse).Message;
                return _mapper.MapResponseToModel(todoDeleted);
            }

            await todoNotFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}