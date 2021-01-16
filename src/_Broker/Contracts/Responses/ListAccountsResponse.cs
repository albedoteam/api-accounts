using System.Collections.Generic;

namespace Accounts.Contracts.Responses
{
    public interface ListAccountsResponse
    {
        List<AccountResponse> Items { get; set; }
    }
}