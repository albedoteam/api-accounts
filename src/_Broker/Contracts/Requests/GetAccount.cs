namespace Accounts.Requests
{
    public interface GetAccountRequest
    {
        string Id { get; set; }
        bool ShowDeleted { get; set; }
    }
}