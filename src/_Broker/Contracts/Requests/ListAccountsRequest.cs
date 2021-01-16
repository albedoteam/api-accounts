namespace Accounts.Contracts.Requests
{
    public interface ListAccountsRequest
    {
        bool ShowDeleted { get; set; }
    }
}