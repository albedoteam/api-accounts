namespace Accounts.Contracts.Requests
{
    public interface ListAccountsRequest
    {
        bool ShowDeleted { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}