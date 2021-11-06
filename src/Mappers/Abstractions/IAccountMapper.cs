namespace Accounts.Api.Mappers.Abstractions
{
    using AccountGrpc;
    using Models;
    using Services.AccountService.Requests;

    public interface IAccountMapper
    {
        // request
        GetRequest MapRequest(Get request);
        CreateRequest MapRequest(Create request);
        DeleteRequest MapRequest(Delete request);
        UpdateRequest MapRequest(Update request);
        ListRequest MapRequest(List request);

        // response
        Account MapResponse(AccountResponse response);
        Paged<Account> MapResponse(ListAccountsResponse response);
    }
}