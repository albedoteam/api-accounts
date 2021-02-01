using System.Collections.Generic;
using Accounts.Api.Models;
using Accounts.Api.Services.AccountService.Requests;
using AlbedoTeam.Accounts.Contracts.Events;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;

namespace Accounts.Api.Mappers
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