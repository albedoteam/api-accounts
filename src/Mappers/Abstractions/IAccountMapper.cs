using System.Collections.Generic;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using Accounts.Events;
using Accounts.Requests;
using Accounts.Responses;

namespace Accounts.Api.Mappers.Abstractions
{
    public interface IAccountMapper
    {
        // Broker to Model
        Account MapResponseToModel(AccountResponse response);
        List<Account> MapResponseToModel(List<AccountResponse> response);
        Account MapResponseToModel(AccountDeleted response);
        Account MapResponseToModel(AccountUpdated response);

        // MediatR to Broker
        CreateAccount MapRequestToBroker(Create request);
        DeleteAccount MapRequestToBroker(Delete request);
        UpdateAccount MapRequestToBroker(Update request);
        GetAccount MapRequestToBroker(Get request);
        ListAccounts MapRequestToBroker(List request);
    }
}