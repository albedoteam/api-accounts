﻿using System.Threading.Tasks;
using Accounts.Api.Mappers.Abstractions;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Sdk.FailFast.Abstractions;
using MassTransit;

namespace Accounts.Api.Services.AccountService.Handlers
{
    public class GetHandler : QueryHandler<GetAccount, Account>
    {
        private readonly IRequestClient<GetAccountRequest> _client;
        private readonly IAccountMapper _mapper;

        public GetHandler(IRequestClient<GetAccountRequest> client, IAccountMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        protected override async Task<Account> Handle(GetAccount request)
        {
            var (todoResponse, notFoundResponse) =
                await _client.GetResponse<AccountResponse, AccountNotFound>(_mapper.MapRequestToBroker(request));

            if (todoResponse.IsCompletedSuccessfully)
            {
                var item = await todoResponse;
                return _mapper.MapResponseToModel(item.Message);
            }

            await notFoundResponse;
            return null; // returning null the Response.NotFound will be true
        }
    }
}