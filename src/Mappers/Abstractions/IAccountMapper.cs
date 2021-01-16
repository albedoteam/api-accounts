using System.Collections.Generic;
using Accounts.Contracts.Events;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Api.Models;
using AlbedoTeam.Accounts.Api.Services.AccountService.Requests;

namespace AlbedoTeam.Accounts.Api.Mappers.Abstractions
{
    public interface IAccountMapper
    {
        // Broker to Model
        Account MapResponseToModel(AccountResponse response);
        List<Account> MapResponseToModel(List<AccountResponse> response);

        // MediatR to Broker
        CreateAccountRequest MapRequestToBroker(CreateAccount request);
        DeleteAccountRequest MapRequestToBroker(DeleteAccount request);
        UpdateAccountRequest MapRequestToBroker(UpdateAccount request);
        GetAccountRequest MapRequestToBroker(GetAccount request);
        ListAccountsRequest MapRequestToBroker(ListAccounts request);
        Account MapResponseToModel(AccountDeleted response);
        Account MapResponseToModel(AccountUpdated response);
    }
}