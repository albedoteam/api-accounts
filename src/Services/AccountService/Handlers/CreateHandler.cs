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
    public class CreateHandler : CommandHandler<CreateAccount, Account>
    {
        private readonly IRequestClient<CreateAccountRequest> _client;
        private readonly IAccountMapper _mapper;

        public CreateHandler(IRequestClient<CreateAccountRequest> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(CreateAccount request)
        {
            var (todoResponse, todoExistsResponse) =
                await _client.GetResponse<AccountResponse, AccountExistsResponse>(_mapper.MapRequestToBroker(request));

            if (todoExistsResponse.IsCompletedSuccessfully)
            {
                await todoExistsResponse;
                throw new ResourceExistsException();
            }

            var todo = await todoResponse;
            return _mapper.MapResponseToModel(todo.Message);
        }
    }
}