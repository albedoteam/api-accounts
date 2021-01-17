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
    public class UpdateHandler : CommandHandler<UpdateAccount, Account>
    {
        private readonly IRequestClient<UpdateAccountRequest> _client;
        private readonly IAccountMapper _mapper;

        public UpdateHandler(IRequestClient<UpdateAccountRequest> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(UpdateAccount request)
        {
            var (todoUpdatedResponse, todoNotFoundResponse) =
                await _client.GetResponse<AccountUpdated, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (todoUpdatedResponse.IsCompletedSuccessfully)
            {
                var todoUpdated = (await todoUpdatedResponse).Message;
                return _mapper.MapResponseToModel(todoUpdated);
            }

            await todoNotFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}