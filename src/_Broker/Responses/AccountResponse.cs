using System;

namespace Accounts.Contracts.Responses
{
    public interface AccountResponse
    {
        string Id { get; set; }
        string Name { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}