using System;
using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;

namespace Accounts.Api.GraphQL.AccountOperations
{
    public class CreateAccountMutation
    {
        private readonly IRequestClient<CreateAccount> _client;
        private readonly IAccountMapper _mapper;

        public CreateAccountMutation(IRequestClient<CreateAccount> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Account> CreateAccount(Create request)
        {
            var (successResponse, errorResponse) =
                await _client.GetResponse<AccountResponse, ErrorResponse>(_mapper.MapRequestToBroker(request));

            if (errorResponse.IsCompletedSuccessfully)
            {
                var error = await errorResponse;
                throw new Exception($"{error.Message.ErrorType}-{error.Message.ErrorMessage}");
            }
    
            var account = (await successResponse).Message;
            return _mapper.MapResponseToModel(account);
        }
    }
}