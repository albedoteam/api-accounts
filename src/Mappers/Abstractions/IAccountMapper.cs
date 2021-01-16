using System.Collections.Generic;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;

namespace Accounts.Api.Mappers.Abstractions
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
    }
}