using System.Collections.Generic;

namespace Accounts.Contracts.Responses
{
    public interface ListAccountsResponse
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int RecordsInPage { get; set; }
        int TotalPages { get; set; }
        List<AccountResponse> Items { get; set; }
    }
}